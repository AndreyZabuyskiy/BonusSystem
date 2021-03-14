using System;
using System.ComponentModel.DataAnnotations;

namespace BonusSystem.Models.ViewModels
{
    public class BonusCardMoneyView
    {
        public BonusCard Card { get; set; }

        [Required (ErrorMessage = "Не указана сумма")]
        [Range(1, 1000000)]
        public int Money { get; set; }
    }
}
