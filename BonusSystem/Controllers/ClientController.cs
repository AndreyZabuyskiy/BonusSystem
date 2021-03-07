using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BonusSystem.Models;
using BonusSystem.Models.Db;
using BonusSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BonusSystem.Controllers
{
    public class ClientController : Controller
    {
        private ApplicationContext _db;

        public ClientController(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> View(Guid id)
        {
            if (id != null)
            {
                var client = await _db.Clients.Include(c => c.BonusCard)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (client != null)
                {
                    return View(client);
                }
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if(id != null)
            {
                var client = await _db.Clients.FirstOrDefaultAsync(c => c.Id == id);

                if(client != null)
                {
                    return View(client);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Client client)
        {
            if(client != null)
            {
                var editClient = await _db.Clients.FirstOrDefaultAsync(c => c.Id == client.Id);

                if(editClient != null)
                {
                    editClient.Copy(client);
                    await _db.SaveChangesAsync();
                }
            }

            return RedirectToAction("View", new { id = client.Id });
        }

        [HttpGet]
        public async Task<IActionResult> CreditFundsToCard(Guid id)
        {
            if (id != null) 
            {
                var card = await _db.BonusCards.FirstOrDefaultAsync(c => c.Id == id);

                if (card != null)
                {
                    ViewBonusCard_Money model = new ViewBonusCard_Money()
                    {
                        Card = card
                    };

                    return View(model);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreditFundsToCard(ViewBonusCard_Money model)
        {
            if(model != null)
            {
                if(model.Card == null)
                    return RedirectToAction("Test");

                var card = await _db.BonusCards.Include(c => c.Client)
                                    .FirstOrDefaultAsync(c => c.Id == model.Card.Id);

                card.Balance += model.Money;
                await _db.SaveChangesAsync();

                return RedirectToAction("View", new { id = card.Client.Id });
            }

            return NotFound();
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}
