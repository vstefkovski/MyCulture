using MyCulture.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCulture.Domain.DTO
{
    public class AddToCultureCartDto
    {
        public Culture SelectedCulture { get; set; }
        public Guid CultureId { get; set; }
        public int Quantity { get; set; }
    }
}
