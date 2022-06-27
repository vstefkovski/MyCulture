using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCulture.Domain.DomainModels
{
    public class Culture:BaseEntity
    {
        [Required]
        public string  CultureName { get; set; }
        [Required]
        public string CultureImage { get; set; }
        [Required]
        public string  CultureDescription { get; set; }
        [Required]
        public int  CulturePrice { get; set; }
        [Required]
        public string Rating { get; set; }
        public virtual ICollection<CultureInCultureCart> CultureInCultureCarts { get; set; }
        public virtual ICollection<CultureInOrder> Orders { get; set; }

        public Culture()
        {

        }
    }
}
