using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TransferMarkt.Data;
using TransferMarkt.Models;
using TransferMarkt.Repositories.Abstract;
using TransferMarkt.Repositories.Implementation;

namespace TransferMarkt.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService _context;
        private readonly IWebHostEnvironment _webHostEnv;
        private readonly IBidService _bidService;
        private readonly INegotiate _negotiate;
        public PlayersController(IPlayerService context, IWebHostEnvironment webHostEnv, IBidService bidService, INegotiate negotiate)
        {
            _context = context;
            _webHostEnv = webHostEnv;
            _bidService = bidService;
            _negotiate = negotiate;
        }

        // GET: Players
        public async Task<IActionResult> Index(string searchstring,int ? pageNo)
        {
            var applicationDbContext = _context.GetData();
            int pageSize = 3;
            if (!string.IsNullOrEmpty(searchstring))
            {
                applicationDbContext=applicationDbContext.Where(l=>l.Name.Contains(searchstring));
                return View(await Pager<Players>.CreateAsync(applicationDbContext.Where(l=>l.IsSold==false).AsNoTracking(), pageNo ?? 1, pageSize));
            }
            return View(await Pager<Players>.CreateAsync(applicationDbContext.Where(l => l.IsSold == false).AsNoTracking(), pageNo ?? 1, pageSize));

        }
        public async Task<IActionResult> MyListings(int? pageNo)
        {
            var applicationDbContext = _context.GetData();
            int pageSize = 3;

            return View("Index",await Pager<Players>.CreateAsync(applicationDbContext.Where(l => l.IdentityUserId== User.FindFirstValue(ClaimTypes.NameIdentifier)).AsNoTracking(), pageNo ?? 1, pageSize));

        }
        public async Task<IActionResult> MyBids(int? pageNo)
        {
            var applicationDbContext = _bidService.GetBidList();
            int pageSize = 3;

            return View(await Pager<Bid>.CreateAsync(applicationDbContext.Where(l => l.IdentityUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).AsNoTracking(), pageNo ?? 1, pageSize));

        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var players = await _context.Details(id);
            if(players==null)
            {
                return NotFound();
            }

            return View(players);
        }
        [HttpPost]
        public async Task<IActionResult> AddBid([Bind("Id,Price,ListingId,IdentityUserId")] Bid bids)
        {
            if (ModelState.IsValid)
            {
                await _bidService.AddBid(bids);
            }
            var player= await _context.Details(bids.ListingId);
            player.Price = bids.Price;
            await _context.Save();
            return View("Details", player);
            
        }

        [HttpPost]
        public async Task<ActionResult> AddComment([Bind("Id, Ask, ListingId, IdentityUserId")] Models.Negotiate comment)
        {
            if (ModelState.IsValid)
            {
                await _negotiate.Add(comment);
            }
            var listing = await _context.Details(comment.ListingId);
            return View("Details", listing);
        }
        public async Task<IActionResult> CloseBidding(int id)
        {
            var player = await _context.Details(id);
            player.IsSold = true;
            await _context.Save();
            return View("Details",player);    
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayersVM players)
        {
            if(players.ImagePath != null)
            {
                string pathDir = Path.Combine(_webHostEnv.WebRootPath,"Images");
                string fileName = players.ImagePath.FileName;
                string filePath = Path.Combine(pathDir, fileName);
                using(var filestream= new FileStream( filePath, FileMode.Create))
                {
                    players.ImagePath.CopyTo(filestream);
                }
                var obj=new Players
                {
                    Name = players.Name,
                    Country = players.Country,
                    Age = players.Age,
                    Height = players.Height,
                    Position = players.Position,
                    currentClub=players.currentClub,
                    contractYear = players.contractYear,
                    Price=players.Price,
                    CareerGoals = players.CareerGoals,
                    Appearance = players.Appearance,
                    ImagePath = fileName,
                    IdentityUserId = players.IdentityUserId,
                };
                await _context.add(obj);
                return RedirectToAction("Index");
            }
            return View(players);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Details(id);
            if (players == null)
            {
                return NotFound();
            }
            
            return View(players);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country,Age,Height,Position,currentClub,contractYear,Price,Appearance,CareerGoals,ImagePath,IsSold,IsFreeAgent,IdentityUserId")] Players players)
        {
            if (id != players.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.UpdatePlayer(players);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!_context.UpdatePlayer(players))
                    //{
                    //    return new NotFoundResult(); // Return appropriate result
                    //}

                    if (!ModelState.IsValid)
                    {
                        return NotFound(); 
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View("Index");
        }

        // GET: Players/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Playerss == null)
        //    {
        //        return NotFound();
        //    }

        //    var players = await _context.Playerss
        //        .Include(p => p.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (players == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(players);
        //}

        // POST: Players/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Playerss == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Playerss'  is null.");
        //    }
        //    var players = await _context.Playerss.FindAsync(id);
        //    if (players != null)
        //    {
        //        _context.Playerss.Remove(players);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PlayersExists(int id)
        //{
        //  return (_context.Playerss?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
