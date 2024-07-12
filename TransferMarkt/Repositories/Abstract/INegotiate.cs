using TransferMarkt.Models;

namespace TransferMarkt.Repositories.Abstract
{
    public interface INegotiate
    {
        Task Add(Negotiate negotiate);
    }
}
