using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Domain.Movie> Movies { get; set; }
        public virtual DbSet<Domain.Order> Orders { get; set; }
        public virtual DbSet<Domain.OrderItem> OrderItems { get; set; }
        public virtual DbSet<MovieTicket> MovieTickets { get; set; }

    }
}