using System.Data.Entity;
using TrabajoFarmacia2.Models;

namespace TrabajoFarmacia2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=DefaultConnection") { }

        public DbSet<Producto> Productos { get; set; }
    }
}