using System;

namespace BonusSystem.Models.Exceptions
{
    public class ModelNullException : Exception
    {
        public override string Message => "Model is null";
    }
}
