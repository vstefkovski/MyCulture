using Microsoft.AspNetCore.Identity;
using MyCulture.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCulture.Domain.Identity
{
    public class MyCultureApplicationUser:IdentityUser
    {
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual CultureCart UserCart { get; set; }
    }
}
