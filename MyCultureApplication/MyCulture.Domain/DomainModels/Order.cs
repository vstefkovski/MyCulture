using MyCulture.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCulture.Domain.DomainModels
{
    public class Order:BaseEntity
    {
        public string UserId { get; set; }
        public MyCultureApplicationUser User { get; set; }
        public virtual ICollection<CultureInOrder> Cultures { get; set; }

    }
}
