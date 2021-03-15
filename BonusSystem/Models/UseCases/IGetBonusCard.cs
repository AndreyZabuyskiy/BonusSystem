using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.UseCases
{
    public interface IGetBonusCard
    {
        public Task<BonusCard> GetBonusCardAsync(Guid id, bool isIncludeClient);
    }
}
