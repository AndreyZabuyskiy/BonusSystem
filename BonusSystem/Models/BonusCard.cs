using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models
{
    public class BonusCard
    {
        public Guid Id { get; set; }

        public int Number { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Balance { get; set; }

        public Guid ClientId { get; set; }
        public Client Client { get; set; }
    }
}
