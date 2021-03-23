using System;
using System.Threading.Tasks;
using BonusSystem.Models;
using BonusSystem.Models.Exceptions;
using BonusSystem.Models.Services;
using BonusSystem.Models.UseCases;
using BonusSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BonusSystem.Controllers
{
    public class CardController : Controller
    {
        private readonly ICredit _credit;
        private readonly IDebit _debit;
        private readonly IGetBonusCard _getBonusCard;

        public CardController([FromServices]ICredit credit,
                                 [FromServices]IDebit debit,
                                 [FromServices]IGetBonusCard getBonusCard)
        {
            _credit = credit;
            _debit = debit;
            _getBonusCard = getBonusCard;
        }

        [HttpGet]
        public async Task<IActionResult> Credit(Guid id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            try
            {
                var card = await _getBonusCard.GetBonusCardAsync(id);
                BonusCardMoneyView model = new BonusCardMoneyView() { Card = card };
                return View(model);
            }
            catch (CardNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Credit(BonusCardMoneyView model)
        {
            if (model is null) return NotFound();

            try
            {
                BonusCard card = await _credit.CreditAsync(model);
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

            try
            {
                var card = await _getBonusCard.GetBonusCardAsync(id);
                BonusCardMoneyView model = new BonusCardMoneyView() { Card = card };
                return View(model);
            }
            catch (CardNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Debit(BonusCardMoneyView model)
        {
            if (model is null) return NotFound();

            try
            {
                BonusCard card = await _debit.DebitAsync(model);
                return RedirectToAction("View", new { controller = "Client", id = card.Client.Id });
            }
            catch (CardNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
