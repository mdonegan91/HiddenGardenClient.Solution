using Microsoft.AspNetCore.Identity;

namespace HiddenGarden.Models
{
  public class ApplicationUser : IdentityUser
  {
      // We won't be adding properties to our custom ApplicationUser class in our To Do List application, but you definitely should explore doing so in your own projects:
      // public string Website { get; set; }
      // public string Image { get; set; }
      // public DateOnly Birthday { get; set; }
  }
}