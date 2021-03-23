using System;

namespace BonusSystem.Models.Exceptions
{
    public class IdNullOrEmptyException : Exception
    {
        public override string Message => "Id is null or empty";
    }
}
