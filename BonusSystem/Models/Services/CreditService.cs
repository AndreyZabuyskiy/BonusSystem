using BonusSystem.Models.Db;
using BonusSystem.Models.Exceptions;
using BonusSystem.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BonusSystem.Models.Services
{
    public class CreditService : ICredit
    {
        private ApplicationContext _db;

        public CreditService(ApplicationContext db) => _db = db;

        public async Task<BonusCard> Credit(ViewBonusCard_Money model)
        {
            if(model is null) throw new CardNotFoundException();

            var card = await _db.BonusCards.Include(c => c.Client)
                                    .FirstOrDefaultAsync(c => c.Id == model.Card.Id);

            if(card is null) throw new CardNotFoundException();

            if (card.ExpirationDate > DateTime.Now)
            {
                card.Balance += model.Money;
                await _db.SaveChangesAsync();
            }

            return card;
        }
    }
}
