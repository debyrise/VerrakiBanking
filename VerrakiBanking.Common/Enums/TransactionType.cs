using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerrakiBanking.Common.Enums
{
    public enum TransactionType
    {
        Deposit = 1,      
        Withdrawal = 2,   
        Transfer = 3,     
        Payment = 4,      
        Refund = 5,       
        Fee = 6,          
        Adjustment = 7,   
        Interest = 8      
    }

}
