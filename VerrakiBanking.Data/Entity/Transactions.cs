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
        public int Id { get; set; } 
        public int AccountId { get; set; } 
        public decimal Amount { get; set; } 
        public DateTime Date { get; set; } 
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; } 

        public virtual Accounts Account { get; set; }
    }

}
