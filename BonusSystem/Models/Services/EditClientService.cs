using BonusSystem.Models.Db;
using BonusSystem.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BonusSystem.Models.Services
{
    public class EditClientService : IEditClient
    {
        private ApplicationContext _db;

        public EditClientService(ApplicationContext db) => _db = db;

        public async Task Edit(Client client)
        {
            if (client is null) throw new ClientNotFoundException();

            var editClient = await _db.Clients.FirstOrDefaultAsync(c => c.Id == client.Id);

            if (client is null) throw new ClientNotFoundException();

            editClient.Copy(client);
            await _db.SaveChangesAsync();
        }
    }
}
