using BonusSystem.Models.Db;
using BonusSystem.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.Services
{
    public class DebitService : IDebit
    {
        private ApplicationContext _db;

        public DebitService(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<BonusCard> Debit(ViewBonusCard_Money model)
        {
            if (model != null) 
            {
                var card = await _db.BonusCards.Include(c => c.Client)
                                    .FirstOrDefaultAsync(c => c.Id == model.Card.Id);

                if (card != null)
                {
                    if (card.ExpirationDate > DateTime.Now && card.Balance > model.Money)
                    {
                        card.Balance -= model.Money;
                        await _db.SaveChangesAsync();
                    }

                    return card;
                }
            }

            throw new NullReferenceException();
        }
    }
}
