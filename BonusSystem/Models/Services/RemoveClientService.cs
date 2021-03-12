using BonusSystem.Models.Db;
using BonusSystem.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BonusSystem.Models.Services
{
    public class RemoveClientService : IRemoveClient
    {
        private ApplicationContext _db;

        public RemoveClientService(ApplicationContext db) => _db = db;

        public async Task Remove(Guid id)
        {
            if (id == null || id == Guid.Empty) throw new ClientNotFoundException();

            var client = await _db.Clients.FirstOrDefaultAsync(c => c.Id == id);

            if(client is null) throw new ClientNotFoundException();

            _db.Clients.Remove(client);
            await _db.SaveChangesAsync();
        }
    }
}
