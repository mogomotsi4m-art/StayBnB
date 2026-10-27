using Microsoft.AspNetCore.Authorization; using Microsoft.AspNetCore.Identity; using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore; using Staybnb.Data; using Staybnb.Models; using Staybnb.Services; using Staybnb.ViewModels;
namespace Staybnb.Controllers;
[Authorize(Roles="Guest")]
public class BookingsController(ApplicationDbContext db,UserManager<ApplicationUser> users,BookingService bookingService):Controller {
 [HttpGet] public IActionResult Create(int propertyId)=>View(new BookingViewModel{PropertyId=propertyId});
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Create(BookingViewModel vm){if(!ModelState.IsValid)return View(vm);var (booking,error)=await bookingService.CreateAsync(users.GetUserId(User)!,vm.PropertyId,vm.CheckInDate,vm.CheckOutDate,vm.NumberOfGuests);if(error!=null){ModelState.AddModelError("",error);return View(vm);}return RedirectToAction(nameof(Details),new{id=booking!.Id});}
 public async Task<IActionResult> Mine(){var uid=users.GetUserId(User)!;return View(await db.Bookings.Include(b=>b.Property!).ThenInclude(p=>p.Images).Where(b=>b.GuestId==uid).OrderByDescending(b=>b.CreatedAt).ToListAsync());}
 public async Task<IActionResult> Details(int id){var uid=users.GetUserId(User)!;var b=await db.Bookings.Include(x=>x.Property).Include(x=>x.GuestCheckin).FirstOrDefaultAsync(x=>x.Id==id&&x.GuestId==uid);if(b==null)return NotFound();return View(b);}
}
