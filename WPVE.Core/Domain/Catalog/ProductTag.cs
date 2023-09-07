using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WPVE.Core.Domain.Catalog
{
    public class ProductTag:BaseEntity
    {
        public ProductTag()
        {
        }
        [Display(Name = "نام تگ")]
        public string Name { get; set; }
    }
}

