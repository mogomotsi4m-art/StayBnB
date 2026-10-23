using Microsoft.EntityFrameworkCore;
using Staybnb.Data; using Staybnb.Models;
namespace Staybnb.Services;
public class ActivityLogger(ApplicationDbContext db) { public async Task LogAsync(string? userId,string action,string type,string? details=null){db.ActivityLogs.Add(new ActivityLog{UserId=userId,Action=action,ActivityType=type,Details=details}); await db.SaveChangesAsync();} }
public class NotificationService(ApplicationDbContext db) { public async Task AddAsync(string userId,string title,string type,string? link=null){db.Notifications.Add(new Notification{UserId=userId,Title=title,Type=type,LinkUrl=link}); await db.SaveChangesAsync();} }
public class SecureFileStorage(IWebHostEnvironment env) {
 private static readonly HashSet<string> ImageExtensions=new(StringComparer.OrdinalIgnoreCase){".jpg",".jpeg",".png",".webp"};
 public async Task<string> SavePropertyImageAsync(IFormFile file,CancellationToken ct=default){if(file.Length<=0||file.Length>5*1024*1024)throw new InvalidOperationException("Image must be between 1 byte and 5 MB."); var ext=Path.GetExtension(file.FileName); if(!ImageExtensions.Contains(ext))throw new InvalidOperationException("Only JPG, PNG, and WEBP images are accepted."); var dir=Path.Combine(env.WebRootPath,"uploads","properties"); Directory.CreateDirectory(dir); var name=Guid.NewGuid().ToString("N")+ext.ToLowerInvariant(); await using var stream=File.Create(Path.Combine(dir,name)); await file.CopyToAsync(stream,ct); return "/uploads/properties/"+name; }
 public async Task<string> SavePrivateDocumentAsync(IFormFile file,CancellationToken ct=default){if(file.Length<=0||file.Length>5*1024*1024)throw new InvalidOperationException("Document must be between 1 byte and 5 MB."); var ext=Path.GetExtension(file.FileName); if(!new[]{".pdf",".jpg",".jpeg",".png"}.Contains(ext,StringComparer.OrdinalIgnoreCase))throw new InvalidOperationException("Only PDF, JPG, and PNG documents are accepted."); var dir=Path.Combine(env.ContentRootPath,"App_Data","private-documents"); Directory.CreateDirectory(dir); var name=Guid.NewGuid().ToString("N")+ext.ToLowerInvariant(); await using var stream=File.Create(Path.Combine(dir,name)); await file.CopyToAsync(stream,ct); return name; }
 public string PrivateDocumentPath(string name)=>Path.Combine(env.ContentRootPath,"App_Data","private-documents",Path.GetFileName(name));
}
public class BookingService(ApplicationDbContext db, ActivityLogger logger, NotificationService notifications) {
 public async Task<(Booking? Booking,string? Error)> CreateAsync(string guestId,int propertyId,DateTime checkIn,DateTime checkOut,int guests){
  var today=DateTime.UtcNow.Date; if(checkIn.Date<today||checkOut.Date<=checkIn.Date)return(null,"Choose a future check-in date and a check-out date after check-in."); if(guests<1)return(null,"At least one guest is required.");
  var p=await db.Properties.Include(x=>x.Bookings).FirstOrDefaultAsync(x=>x.Id==propertyId&&x.IsActive); if(p==null)return(null,"This property is not currently available."); if(guests>p.Capacity)return(null,$"This property accommodates up to {p.Capacity} guests.");
  var conflict=p.Bookings.Any(b=>b.Status==WorkflowStatus.Approved && checkIn.Date<b.CheckOutDate.Date && checkOut.Date>b.CheckInDate.Date); if(conflict)return(null,"Those dates overlap an approved booking.");
  var nights=(checkOut.Date-checkIn.Date).Days; var subtotal=p.PricePerNight*nights; var total=subtotal+p.CleaningFee+p.ServiceFee;
  var booking=new Booking{PropertyId=p.Id,GuestId=guestId,CheckInDate=checkIn.Date,CheckOutDate=checkOut.Date,NumberOfGuests=guests,PricePerNightAtBooking=p.PricePerNight,AccommodationSubtotal=subtotal,CleaningFeeAtBooking=p.CleaningFee,ServiceFeeAtBooking=p.ServiceFee,TotalPrice=total,Status=WorkflowStatus.Pending,Payment=new Payment{Amount=total,Status="NotProcessed"}};
  db.Bookings.Add(booking); await db.SaveChangesAsync(); await logger.LogAsync(guestId,"Created booking #"+booking.Id,"Booking",$"Property={p.Id}; Total={total}"); if(p.UserId!=guestId)await notifications.AddAsync(p.UserId,$"New booking request for {p.Title}","Booking","/Host/Bookings"); return(booking,null);
 }
}
