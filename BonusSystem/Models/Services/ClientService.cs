using BonusSystem.Models.Db;
using BonusSystem.Models.Services;
using BonusSystem.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.Exceptions
{
    public class ClientService : ICreateClient, IEditClient, IRemoveClient
    {
        private ApplicationContext _db;
        private const int _minValueNumber = 100000;
        private const int _maxValueNumber = 999999;

        public ClientService(ApplicationContext db) => _db = db;

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

        public async Task Edit(Client client)
        {
            if (client is null) throw new ClientNotFoundException();

            var editClient = await _db.Clients.FirstOrDefaultAsync(c => c.Id == client.Id);

            if (editClient is null) throw new ClientNotFoundException();

            editClient.Copy(client);
            await _db.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            if (id == null || id == Guid.Empty) throw new ClientNotFoundException();

            var client = await _db.Clients.FirstOrDefaultAsync(c => c.Id == id);

            if (client is null) throw new ClientNotFoundException();

            _db.Clients.Remove(client);
            await _db.SaveChangesAsync();
        }

        private async Task<int> GetNumberCard()
        {
            Random rnd = new Random();

            int number = rnd.Next(_minValueNumber, _maxValueNumber);
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
                        number = rnd.Next(_minValueNumber, _maxValueNumber);

                } while (!isUniqueNumber);
            }

            return number;
        }
    }
}
