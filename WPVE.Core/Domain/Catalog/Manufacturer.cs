using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WPVE.Core.Domain.Catalog
{
    public class Manufacturer : BaseEntity
    {
        public Manufacturer()
        {
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [Display(Name = "عنوان سازنده")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        [Display(Name = "انتشار")]
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        [Display(Name = "حذف شده")]
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        [Display(Name = "چیدمان")]
        public int DisplayOrder { get; set; }
        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        [Display(Name = "کلمات کلیدی")]
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        [Display(Name = "توضیحات متا")]
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        [Display(Name = "عنوان متا")]
        public string MetaTitle { get; set; }

    }
}

