using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.UseCases
{
    public interface Persist
    {
        public Task PersistAsync(Client client);
    }
}
