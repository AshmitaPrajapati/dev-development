using API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<MenuItemEntity> MenuItemEntities { get; set; }
        public DbSet<SubMenuItemEntity> SubMenuItemEntities { get; set; }
        public DbSet<DemoConfigEntity> DemoConfigEntities { get; set; }
    }
}
