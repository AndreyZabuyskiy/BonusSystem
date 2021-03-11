using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.Services
{
    public interface IEditClient
    {
        public Task Edit(Client client);
    }
}
