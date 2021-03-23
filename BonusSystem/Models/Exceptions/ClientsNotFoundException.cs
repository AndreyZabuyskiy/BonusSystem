using System;

namespace BonusSystem.Models.Exceptions
{
    public class ClientsNotFoundException : Exception
    {
        public override string Message => "Clients not found!";
    }
}
