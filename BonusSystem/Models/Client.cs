using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.Models
{
    public class Client
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }

        public BonusCard BonusCard { get; set; }

        public void Copy(Client client)
        {
            FirstName = client.FirstName;
            LastName = client.LastName;
            MiddleName = client.MiddleName;
            PhoneNumber = client.PhoneNumber;
        }
    }
}
