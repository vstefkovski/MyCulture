using MyCulture.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCulture.Domain.DomainModels
{
    public class CultureCart:BaseEntity
    {
        public string  OwnerId { get; set; }
        public MyCultureApplicationUser Owner { get; set; }
        public virtual  ICollection<CultureInCultureCart> CultureInCultureCarts { get; set; }
    }
}
