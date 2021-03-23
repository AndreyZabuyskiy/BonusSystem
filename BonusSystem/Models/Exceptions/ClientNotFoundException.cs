using System;

namespace BonusSystem.Models.Exceptions
{
    public class ClientNotFoundException : Exception
    {
        public override string Message => "Client not found!";
    }
}
