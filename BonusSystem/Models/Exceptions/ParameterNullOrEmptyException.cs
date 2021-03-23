using System;

namespace BonusSystem.Models.Exceptions
{
    public class ParameterNullOrEmptyException : Exception
    {
        public override string Message => "Parameter is null or empty";
    }
}
