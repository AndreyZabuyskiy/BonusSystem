using BonusSystem.Models.Db;
using BonusSystem.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.Services
{
    public class CreditFundsService : ICreditFunds
    {
        private ApplicationContext _db;

        public CreditFundsService(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<BonusCard> CreditFunds(ViewBonusCard_Money model)
        {
            if (model is null) return null;

            var card = await _db.BonusCards.Include(c => c.Client)
                                    .FirstOrDefaultAsync(c => c.Id == model.Card.Id);

            if (card != null)
            {
                if (card.ExpirationDate > DateTime.Now)
                {
                    card.Balance += model.Money;
                    await _db.SaveChangesAsync();
                }

                return card;
            }

            return null;
        }
    }
}
