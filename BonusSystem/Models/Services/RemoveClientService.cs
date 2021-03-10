using BonusSystem.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.Services
{
    public class RemoveClientService : IRemoveClient
    {
        private ApplicationContext _db;

        public RemoveClientService(ApplicationContext db)
        {
            _db = db;
        }

        public async Task Remove(Guid id)
        {
            if (id != null)
            {
                var client = await _db.Clients.FirstOrDefaultAsync(c => c.Id == id);

                if (client != null)
                {
                    _db.Clients.Remove(client);
                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}
