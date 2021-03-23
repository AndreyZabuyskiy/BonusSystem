using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.Exceptions
{
    public class ClientNotFoundException : Exception
    {
        public override string Message => "Пользователь не найден";
    }
}
