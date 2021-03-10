using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BonusSystem.Models;
using BonusSystem.Models.Db;
using BonusSystem.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using BonusSystem.Models.Services;

namespace BonusSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ApplicationContext _db;
        private ICreateClient _createClient;

        public HomeController(ILogger<HomeController> logger, ApplicationContext db, 
                                [FromServices]ICreateClient createClient)
        {
            _logger = logger;
            _db = db;
            _createClient = createClient;
        }

        public async Task<IActionResult> Index() => View(await _db.Clients.Include(c => c.BonusCard).ToListAsync());

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ViewCreateClient_BonusCard model)
        {
            if (ModelState.IsValid)
            {                
                await _createClient.Create(model);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveClient(Guid id)
        {
            if(id != null)
            {
                var client = await _db.Clients.FirstOrDefaultAsync(c => c.Id == id);
                
                if(client != null)
                {
                    _db.Clients.Remove(client);
                    await _db.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Search() => View();

        [HttpPost]
        public async Task<IActionResult> SearchByPhoneNumber(ViewSearchClient model)
        {
            if(model != null)
            {
                var client = await _db.Clients.FirstOrDefaultAsync(c => c.PhoneNumber == model.PhoneNumber);

                if(client != null)
                    return RedirectToAction("View", new { controller = "Client", id = client.Id });
            }

            return RedirectToAction("Search");
        }

        [HttpPost]
        public async Task<IActionResult> SearchByNumberCard(ViewSearchClient model)
        {
            if (model != null)
            {
                var card = await _db.BonusCards.Include(c => c.Client)
                                               .FirstOrDefaultAsync(c => c.Number == model.NumberCard);

                if (card != null)
                    return RedirectToAction("View", new { controller = "Client", id = card.Client.Id });
            }

            return RedirectToAction("Search");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
