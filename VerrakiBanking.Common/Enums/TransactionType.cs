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
        Withdrawal = 2,   // A withdrawal from the account
        Transfer = 3,     // A transfer to/from another account
        Payment = 4,      // A payment made from the account
        Refund = 5,       // A refund to the account
        Fee = 6,          // A fee charged on the account
        Adjustment = 7,   // An adjustment made to the balance (e.g., correction)
        Interest = 8      // Interest earned or paid to the account
    }

}
