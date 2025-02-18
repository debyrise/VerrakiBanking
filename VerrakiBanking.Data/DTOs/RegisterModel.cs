using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerrakiBanking.Data.DTOs
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TransactionPin { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
