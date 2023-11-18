using api1.Entities;
using Microsoft.EntityFrameworkCore;

namespace api1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public override int SaveChanges()
        {
            var datas = ChangeTracker.Entries<BaseEntity>().ToList();
            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Deleted:
                        data.Entity.DeleteDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        data.Entity.UpdateDate = DateTime.Now;
                        break;
                    case EntityState.Added:
                        data.Entity.CreateDate = DateTime.Now;
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChanges();
        }
    }
}
