using BonusSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.Services
{
    public interface ICreateClient
    {
        public Task Create(ViewCreateClient_BonusCard model);
    }
}
