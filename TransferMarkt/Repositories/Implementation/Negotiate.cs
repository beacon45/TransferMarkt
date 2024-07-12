using TransferMarkt.Data;
using TransferMarkt.Models;
using TransferMarkt.Repositories.Abstract;

namespace TransferMarkt.Repositories.Implementation
{
    public class Negotiate : INegotiate
    {
        private readonly ApplicationDbContext _context;

        public Negotiate(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Models.Negotiate negotiate)
        {
            _context.Negotiates.Add(negotiate);
            await _context.SaveChangesAsync();

        }
    }
}
