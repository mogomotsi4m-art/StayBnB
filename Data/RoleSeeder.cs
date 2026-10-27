using Microsoft.AspNetCore.Identity;
using Staybnb.Models;
namespace Staybnb.Data;
public static class RoleSeeder {
 public static readonly string[] Roles = ["Guest","Host","Admin","SuperAdmin"];
 public static async Task SeedAsync(IServiceProvider services, IConfiguration config) {
  var roles=services.GetRequiredService<RoleManager<IdentityRole>>(); var users=services.GetRequiredService<UserManager<ApplicationUser>>();
  foreach(var role in Roles) if(!await roles.RoleExistsAsync(role)) { var result=await roles.CreateAsync(new IdentityRole(role)); if(!result.Succeeded) throw new InvalidOperationException("Could not seed role: "+string.Join("; ",result.Errors.Select(e=>e.Description))); }
  var email=config["SuperAdmin:Email"]; var password=config["SuperAdmin:Password"];
  if(string.IsNullOrWhiteSpace(email)) return;
  var user=await users.FindByEmailAsync(email);
  if(user==null && !string.IsNullOrWhiteSpace(password)) { user=new ApplicationUser{UserName=email,Email=email,EmailConfirmed=true,FullName="Staybnb Super Administrator"}; var create=await users.CreateAsync(user,password); if(!create.Succeeded) throw new InvalidOperationException("SuperAdmin seed failed: "+string.Join("; ",create.Errors.Select(e=>e.Description))); }
  if(user!=null && !await users.IsInRoleAsync(user,"SuperAdmin")) await users.AddToRoleAsync(user,"SuperAdmin");
 }
}
