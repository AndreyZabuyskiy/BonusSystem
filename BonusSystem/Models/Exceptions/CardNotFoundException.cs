using System;

namespace BonusSystem.Models.Exceptions
{
    public class CardNotFoundException : Exception
    {
        public override string Message => "Bonus card not found!";
    }
}
