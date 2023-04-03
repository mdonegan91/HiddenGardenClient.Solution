using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HiddenGarden.Models
{
  public class HiddenGardenContext: IdentityDbContext<ApplicationUser>
  {
    public DbSet<Backyard> Backyards { get; set; }

    public HiddenGardenContext(DbContextOptions options) : base(options) { }
  }
}