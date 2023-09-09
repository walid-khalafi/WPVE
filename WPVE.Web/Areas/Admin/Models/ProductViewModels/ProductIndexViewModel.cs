using System;
using System.ComponentModel.DataAnnotations;

namespace WPVE.Web.Areas.Admin.Models.ProductViewModels
{
    public class ProductIndexViewModel
    {
        public ProductIndexViewModel()
        {
        }

        /// <summary>
        /// Gets or sets the Identifier
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [Display(Name = "نام محصول")]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the price
        /// </summary>
        [Display(Name = "قیمت")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity
        /// </summary>
        [Display(Name = "موجودی انبار")]
        public int StockQuantity { get; set; }

        /// <summary>
        /// Gets or sets the  product categories identifiers
        /// </summary>
        [Display(Name = "گروه محصول")]
        public string ProductCategories { get; set; }

        /// <summary>
        /// Gets or sets the Manufacturer  identifier
        /// </summary>
        [Display(Name = "شرکت سازنده")]
        public string ManufacturerId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        [Display(Name = "انتشار محصول ")]
        public bool Published { get; set; }
    }
}

