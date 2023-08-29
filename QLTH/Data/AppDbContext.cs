using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QLTH.Models.CommonDTO;
using QLTH.Models.RegionDTO;

namespace QLTH.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Region> Regions { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
               
        } 
    }
}
