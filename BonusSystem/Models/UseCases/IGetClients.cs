using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.UseCases
{
    public interface IGetClients
    {
        public Task<List<Client>> GetClientsAsync(bool isIncludeBonusCard);
    }
}
