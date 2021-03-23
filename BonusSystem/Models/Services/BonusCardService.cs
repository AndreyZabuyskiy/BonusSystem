using BonusSystem.Models.Db;
using BonusSystem.Models.Exceptions;
using BonusSystem.Models.UseCases;
using BonusSystem.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.Services
{
    public class BonusCardService : ICreateBonusCard, ICredit, IDebit, IGetBonusCard
    {
        private ApplicationContext _db;
        private const int _minValueNumber = 100000;
        private const int _maxValueNumber = 999999;

        public BonusCardService(ApplicationContext db) => _db = db;

        public BonusCard Create(int balance)
        {
            int number = GenerateBonusCardNumber();

            BonusCard card = new BonusCard()
            {
                Number = number,
                CreateDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(30),
                Balance = balance
            };

            return card;
        }

        public async Task<BonusCard> CreditAsync(BonusCardMoneyView model)
        {
            if (model is null) throw new ModelNullException();

            var card = await _db.BonusCards.Include(c => c.Client)
                                    .FirstOrDefaultAsync(c => c.Id == model.Card.Id);

            if (card is null) throw new CardNotFoundException();

            if (card.ExpirationDate > DateTime.Now)
            {
                card.Balance += model.Money;
                await _db.SaveChangesAsync();
            }

            return card;
        }

        public async Task<BonusCard> DebitAsync(BonusCardMoneyView model)
        {
            if (model is null) throw new ModelNullException();

            var card = await _db.BonusCards.Include(c => c.Client)
                                    .FirstOrDefaultAsync(c => c.Id == model.Card.Id);

            if (card is null) throw new CardNotFoundException();

            if (card.ExpirationDate > DateTime.Now && card.Balance > model.Money)
            {
                card.Balance -= model.Money;
                await _db.SaveChangesAsync();
            }

            return card;
        }

        private int GenerateBonusCardNumber()
        {
            Random rnd = new Random();

            int number = rnd.Next(_minValueNumber, _maxValueNumber);
            var cards = _db.BonusCards.ToList();

            if (cards != null || cards.Count != 0)
            {
                bool isUniqueNumber = true;

                do
                {
                    foreach (var card in cards)
                    {
                        if (card.Number == number)
                            isUniqueNumber = false;
                    }

                    if (!isUniqueNumber)
                        number = rnd.Next(_minValueNumber, _maxValueNumber);

                } while (!isUniqueNumber);
            }

            return number;
        }

        public async Task<BonusCard> GetBonusCardAsync(Guid id)
        {
            if (id == null || id == Guid.Empty) throw new ParameterNullOrEmptyException();

            BonusCard card = await _db.BonusCards.FirstOrDefaultAsync(c => c.Id == id);

            return card is null ? throw new CardNotFoundException() : card;
        }

        public async Task<BonusCard> GetBonusCardIncludeClientAsync(Guid id)
        {
            if (id == null || id == Guid.Empty) throw new ParameterNullOrEmptyException();

            BonusCard card = await _db.BonusCards.Include(c => c.Client)
                                                 .FirstOrDefaultAsync(c => c.Id == id);

            return card is null ? throw new CardNotFoundException() : card;
        }
    }
}
