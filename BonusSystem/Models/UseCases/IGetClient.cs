using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.UseCases
{
    public interface IGetClient
    {
        public Task<Client> GetClientAsync(Guid id, bool isIncludeClient);
    }
}
