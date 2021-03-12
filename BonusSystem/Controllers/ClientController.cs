using System;
using System.Threading.Tasks;
using BonusSystem.Models;
using BonusSystem.Models.Db;
using BonusSystem.Models.Services;
using BonusSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BonusSystem.Controllers
{
    public class ClientController : Controller
    {
        private ApplicationContext _db;
        private IEditClient _editClient;
        private IDebit _debit;
        private ICreditFunds _creditFunds;
        private IRemoveClient _removeClient;

        public ClientController(ApplicationContext db,
                                [FromServices]IEditClient editClient,
                                [FromServices]IDebit debit,
                                [FromServices]ICreditFunds creditFunds,
                                [FromServices] IRemoveClient removeClient)
        {
            _db = db;
            _editClient = editClient;
            _debit = debit;
            _creditFunds = creditFunds;
            _removeClient = removeClient;
        }

        public async Task<IActionResult> View(Guid id)
        {
            if (id != null)
            {
                var client = await _db.Clients.Include(c => c.BonusCard)
                                      .FirstOrDefaultAsync(c => c.Id == id);

                if (client != null) return View(client);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id != null)
            {
                var client = await _db.Clients.FirstOrDefaultAsync(c => c.Id == id);

                if (client != null) return View(client);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Client client)
        {
            if (client != null)
            {                
                await _editClient.Edit(client);
                return RedirectToAction("View", new { id = client.Id });
            }

            return NotFound();
        }

        public async Task<IActionResult> RemoveClient(Guid id)
        {
            if (id != null)
                await _removeClient.Remove(id);

            return RedirectToAction("Index", new { controller = "Home" });
        }

        [HttpGet]
        public async Task<IActionResult> CreditFunds(Guid id)
        {
            if (id != null)
            {
                var card = await _db.BonusCards.FirstOrDefaultAsync(c => c.Id == id);

                if (card != null)
                {
                    ViewBonusCard_Money model = new ViewBonusCard_Money() { Card = card };
                    return View(model);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreditFunds(ViewBonusCard_Money model)
        {
            if (model != null)
            {
                BonusCard card = await _creditFunds.CreditFunds(model);

                try
                {
                    return RedirectToAction("View", new { id = card.Client.Id });
                }
                catch (NullReferenceException) { }
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Debit(Guid id)
        {
            if (id != null)
            {
                var card = await _db.BonusCards.FirstOrDefaultAsync(c => c.Id == id);

                if (card != null)
                {
                    ViewBonusCard_Money model = new ViewBonusCard_Money() { Card = card };
                    return View(model);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Debit(ViewBonusCard_Money model)
        {
            if (model != null)
            {
                BonusCard card = await _debit.Debit(model);

                try
                {
                    return RedirectToAction("View", new { id = card.Client.Id });
                }
                catch (NullReferenceException) { }
            }

            return NotFound();
        }
    }
}
