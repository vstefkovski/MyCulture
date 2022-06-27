using System;
using System.Collections.Generic;
using System.Text;

namespace MyCulture.Domain.DomainModels
{
    public class CultureInOrder:BaseEntity
    {
        public Guid CultureId { get; set; }
        public Culture SelectedCulture { get; set; }
        public Guid OrderId { get; set; }
        public Order UserOrder { get; set; }
    }
}
