using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.ViewModels
{
    public class ViewBonusCard_Money
    {
        public BonusCard Card { get; set; }

        [Required (ErrorMessage = "Не указана сумма")]
        [Range(1, 1000000)]
        public int Money { get; set; }
    }
}
