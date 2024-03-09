using Microsoft.EntityFrameworkCore;
using TempporalWS.Models;

namespace TempporalWS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Producto> Productos {get;set;}
        public DbSet<ProductoTicket> ProductosTicket { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
