using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Data.Model
{
    public  class User : BaseUpdateAndDeleteModel<int>
    {
        public required string FirstName { get; set; }
        public string MiddleNames { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required string Salt { get; set; }
        public required string Email { get; set; }
    }
}
