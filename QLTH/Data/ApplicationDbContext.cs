using Microsoft.EntityFrameworkCore;
using QLTH.Models;

namespace QLTH.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 

        }

        // Khai báo toàn bộ các kết nối đến các bảng cần truy vấn
        public DbSet<School> Schools { get; set;}

        public DbSet<GioiTinh> GioiTinhs { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GioiTinh>().HasKey(x => new { x.Id, x.Name });
        }

    }
}
