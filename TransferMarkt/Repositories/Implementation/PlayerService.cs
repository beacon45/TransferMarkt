using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using TransferMarkt.Data;
using TransferMarkt.Models;
using TransferMarkt.Repositories.Abstract;

namespace TransferMarkt.Repositories.Implementation
{
    public class PlayerService: IPlayerService
    {

        private readonly ApplicationDbContext _context;

        public PlayerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task add(Players players)
        {
            _context.Playerss.Add(players);
            await _context.SaveChangesAsync();
        }

        public async Task<Players> Details(int? id)
        {
            var play= await _context.Playerss
                .Include(p => p.User)
                .Include(p=>p.Negotiation)
                .Include(p=>p.Bids)
                .ThenInclude(p=>p.User)
                .FirstOrDefaultAsync(m=>m.Id==id);
            return play;
        }
        public IQueryable<Players> GetData()
        {
            var applicationDbContext = _context.Playerss.Include(p => p.User);
            return applicationDbContext;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();   
        }

        public async Task UpdatePlayer(Players player)
        {
            _context.Entry(player).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
