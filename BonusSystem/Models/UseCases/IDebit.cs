using BonusSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.Services
{
    public interface IDebit
    {
        public Task<BonusCard> DebitAsync(BonusCardMoneyView model);
    }
}
