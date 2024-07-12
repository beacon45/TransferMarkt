using TransferMarkt.Models;

namespace TransferMarkt.Repositories.Abstract
{
    public interface IBidService
    {
        Task AddBid(Bid bid);
        IQueryable<Bid> GetBidList();
    }
}
