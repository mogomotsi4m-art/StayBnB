using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore; using Staybnb.Data;
namespace Staybnb.Controllers;
public class PropertiesController(ApplicationDbContext db):Controller { public async Task<IActionResult> Details(int id){var p=await db.Properties.Include(x=>x.Images).Include(x=>x.PropertyAmenities).ThenInclude(x=>x.Amenity).Include(x=>x.HostApplication).FirstOrDefaultAsync(x=>x.Id==id&&x.IsActive&&x.HostApplication!=null&&x.HostApplication.Status==Staybnb.Models.WorkflowStatus.Approved);if(p==null)return NotFound();return View(p);} }
