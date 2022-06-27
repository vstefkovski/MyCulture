using System;
using System.Collections.Generic;
using System.Text;

namespace MyCulture.Domain.DomainModels
{
    public class CultureInCultureCart:BaseEntity
    {
        public Guid CultureId { get; set; }
        public Culture Culture { get; set; }
        public Guid CultureCartId { get; set; }
        public CultureCart CultureCart { get; set; }
        public int Quantity { get; set; }
    }
}
