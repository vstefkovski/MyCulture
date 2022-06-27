using MyCulture.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCulture.Domain.DTO
{
    public class CultureCartDto
    {
        public List<CultureInCultureCart> Cultures { get; set; }
        public double  TotalPrice { get; set; }
    }
}
