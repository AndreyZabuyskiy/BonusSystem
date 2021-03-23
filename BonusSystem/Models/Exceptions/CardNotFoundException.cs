﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.Exceptions
{
    public class CardNotFoundException : Exception
    {
        public override string Message => "Бонусная карта не найдена";
    }
}
