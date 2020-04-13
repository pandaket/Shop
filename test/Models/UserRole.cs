using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace test.Models
{
    public class UserRole:IdentityRole<int>
    {
        public UserRole() { }

        public UserRole(string name)
        {
            Name = name;
        }
    }
}
