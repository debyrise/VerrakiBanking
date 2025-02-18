using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerrakiBanking.Common.Enums;

namespace VerrakiBanking.Data.Entity
{
    public class Transactions
    {
        public int Id { get; set; } // Primary key
        public int AccountId { get; set; } // Foreign key to the associated account
        public decimal Amount { get; set; } // Amount of the transaction
        public DateTime Date { get; set; } // Date of the transaction
        public TransactionType TransactionType { get; set; } // Type of transaction (e.g., deposit, withdrawal)
        public string Description { get; set; } // Optional description or notes for the transaction

        // Navigation property to the associated account
        public virtual Accounts Account { get; set; }
    }

}
