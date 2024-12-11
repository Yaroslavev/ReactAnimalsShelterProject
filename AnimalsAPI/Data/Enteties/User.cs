using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enteties
{
    public class User : IdentityUser
    {
        public DateTime? Birthday { get; set; }
    }
}
