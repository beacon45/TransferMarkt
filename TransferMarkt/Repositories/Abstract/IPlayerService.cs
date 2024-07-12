using Microsoft.AspNetCore.Mvc;
using TransferMarkt.Models;

namespace TransferMarkt.Repositories.Abstract
{
    public interface IPlayerService
    {
        IQueryable<Players> GetData();
        Task add(Players players);
        Task<Players> Details(int? id);
        Task Save();
        Task UpdatePlayer(Players player);
    }
}
