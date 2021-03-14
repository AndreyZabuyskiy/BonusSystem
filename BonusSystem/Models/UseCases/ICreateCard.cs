using BonusSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.UseCases
{
    public interface ICreateCard
    {
        public BonusCard Create(ViewCreateClient_BonusCard model);
    }
}
