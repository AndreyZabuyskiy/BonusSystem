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

namespace BonusSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ApplicationContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index() => View(await _db.Clients.Include(c => c.BonusCard).ToListAsync());

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ViewCreateClient_BonusCard model)
        {
            if (model != null)
            {
                int number = await GetNumberCard();

                BonusCard card = new BonusCard()
                {
                    Number = number,
                    CreateDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    Balance = model.Balance
                };

                Client client = new Client()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    PhoneNumber = model.PhoneNumber,
                    BonusCard = card
                };

                _db.Clients.Add(client);
                await _db.SaveChangesAsync();
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
                {
                    return RedirectToAction("View", new { controller = "Client", id = client.Id });
                }
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
                {                   
                    return RedirectToAction("View", new { controller = "Client", id = card.Client.Id });
                }
            }

            return RedirectToAction("Search");
        }

        private async Task<int> GetNumberCard()
        {
            Random rnd = new Random();

            int number = rnd.Next(100000, 999999);
            var cards = await _db.BonusCards.ToListAsync();

            if (cards != null)
            {
                bool isUniqueNumber = true;

                do
                {
                    foreach (var card in cards)
                    {
                        if (card.Number == number)
                        {
                            isUniqueNumber = false;
                        }
                    }

                    if (!isUniqueNumber)
                    {
                        number = rnd.Next(100000, 999999);
                    }
                } while (!isUniqueNumber);
            }

            return number;
        }

        public IActionResult Test()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
