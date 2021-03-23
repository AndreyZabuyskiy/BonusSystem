using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.Exceptions
{
    public class ClientsNotFoundException : Exception
    {
        public override string Message => "Пользователи не найдены";
    }
}
