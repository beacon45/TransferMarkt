using Microsoft.EntityFrameworkCore;
using TransferMarkt.Data;
using TransferMarkt.Models;
using TransferMarkt.Repositories.Abstract;

namespace TransferMarkt.Repositories.Implementation
{
    public class BidService : IBidService
    {
        private readonly ApplicationDbContext _context;
        public BidService(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public async Task AddBid(Bid bid)
        {
            _context.Add(bid);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Bid> GetBidList()
        {
            var appcontext= from sl in _context.Bids.Include(l=>l.Listing) select sl;
            return appcontext;
        }
    }
}
