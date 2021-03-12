using System;
using System.Threading.Tasks;
using BonusSystem.Models;
using BonusSystem.Models.Db;
using BonusSystem.Models.Exceptions;
using BonusSystem.Models.Services;
using BonusSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BonusSystem.Controllers
{
    public class CardController : Controller
    {
        private ApplicationContext _db;
        private ICredit _credit;
        private IDebit _debit;

        public CardController(ApplicationContext db,
                                 [FromServices]ICredit credit,
                                 [FromServices]IDebit debit)
        {
            _db = db;
            _credit = credit;
            _debit = debit;
        }

        [HttpGet]
        public async Task<IActionResult> Credit(Guid id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            var card = await _db.BonusCards.FirstOrDefaultAsync(c => c.Id == id);

            if (card is null) return NotFound();

            ViewBonusCard_Money model = new ViewBonusCard_Money() { Card = card };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Credit(ViewBonusCard_Money model)
        {
            if (model is null) return NotFound();

            try
            {
                BonusCard card = await _credit.Credit(model);
                return RedirectToAction("View", new { controller = "Client", id = card.Client.Id });
            }
            catch (CardNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Debit(Guid id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            var card = await _db.BonusCards.FirstOrDefaultAsync(c => c.Id == id);

            if (card is null) return NotFound();

            ViewBonusCard_Money model = new ViewBonusCard_Money() { Card = card };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Debit(ViewBonusCard_Money model)
        {
            if (model is null) return NotFound();

            try
            {
                BonusCard card = await _debit.Debit(model);
                return RedirectToAction("View", new { controller = "Client", id = card.Client.Id });
            }
            catch (CardNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
