using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerrakiBanking.Data.Entity
{
    public class Accounts
    {
        public int Id { get; set; } // Primary key

        public string AccountNumber { get; set; } // Unique identifier for the account
        public decimal Balance { get; set; } // Current balance of the account
        public string AccountHolder { get; set; } // Name of the person or entity who owns the account
        public DateTime CreatedAt { get; set; } // Date the account was created
        public string AccountType { get; set; } // Type of account (e.g., "Checking", "Savings")
        public bool IsActive { get; set; } // Indicates if the account is currently active

        // Optional: Navigation properties
        public virtual ICollection<Transactions> Transactions { get; set; } // List of transactions for the account
    }

}
