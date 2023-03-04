using CRUD_API.models;
using CRUD_API.models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
