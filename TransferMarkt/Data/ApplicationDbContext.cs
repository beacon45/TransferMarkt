using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TransferMarkt.Models;

namespace TransferMarkt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
               
        }
        public DbSet<Players> Playerss { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Negotiate> Negotiates { get; set; }
    }
}
