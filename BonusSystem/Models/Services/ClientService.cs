using BonusSystem.Models.Db;
using BonusSystem.Models.Services;
using BonusSystem.Models.UseCases;
using BonusSystem.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.Exceptions
{
    public class ClientService : ICreateClient, IEditClient, IRemoveClient, Persist
    {
        private ApplicationContext _db;

        public ClientService(ApplicationContext db) => _db = db;

        public Client Create(CreateClientBonusCardView model)
        {
            if (model is null) throw new Exception();

            Client client = new Client()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                PhoneNumber = model.PhoneNumber
            };

            return client;
        }

        public async Task EditAsync(Client client)
        {
            if (client is null) throw new ClientNotFoundException();

            var editClient = await _db.Clients.FirstOrDefaultAsync(c => c.Id == client.Id);

            if (editClient is null) throw new ClientNotFoundException();

            editClient.Copy(client);
            await _db.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            if (id == null || id == Guid.Empty) throw new ClientNotFoundException();

            var client = await _db.Clients.FirstOrDefaultAsync(c => c.Id == id);

            if (client is null) throw new ClientNotFoundException();

            _db.Clients.Remove(client);
            await _db.SaveChangesAsync();
        }

        public async Task PersistAsync(Client client)
        {
            if (client.BonusCard != null)
            {
                _db.Clients.Add(client);
                await _db.SaveChangesAsync();
            }
        }
    }
}
