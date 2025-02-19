using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerrakiBanking.Data.Entity
{
    public class Accounts
    {
        public int Id { get; set; } 

        public string AccountNumber { get; set; }
        public decimal Balance { get; set; } 
        public string AccountHolder { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public string AccountType { get; set; } 
        public bool IsActive { get; set; }

        public virtual ICollection<Transactions> Transactions { get; set; } 
    }

}
