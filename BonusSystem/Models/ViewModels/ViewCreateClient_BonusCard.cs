using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models.ViewModels
{
    public class ViewCreateClient_BonusCard
    {
        [Required(ErrorMessage = "Не указан Firstname")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Не указан Lastname")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Не указан Middlename")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        public string MiddleName { get; set; }
        
        [Required(ErrorMessage = "Не указан Phone number")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный phone number")]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "Не указан Balance")]
        [Range(1, 1000000)]
        public int Balance { get; set; }
    }
}
