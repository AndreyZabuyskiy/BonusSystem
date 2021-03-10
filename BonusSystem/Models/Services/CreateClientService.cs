using BonusSystem.Models.Db;
using BonusSystem.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.Services
{
    public class CreateClientService : ICreateClient
    {
        private ApplicationContext _db;

        public CreateClientService(ApplicationContext db)
        {
            _db = db;
        }

        public async Task Create(ViewCreateClient_BonusCard model)
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
                            isUniqueNumber = false;
                    }

                    if (!isUniqueNumber)
                        number = rnd.Next(100000, 999999);

                } while (!isUniqueNumber);
            }

            return number;
        }
    }
}
