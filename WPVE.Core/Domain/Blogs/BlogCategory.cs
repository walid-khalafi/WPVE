using System;
using System.ComponentModel.DataAnnotations;

namespace WPVE.Core.Domain.Blogs
{
    public class BlogCategory:BaseEntity
    {
        public BlogCategory()
        {
        }

        /// <summary>
        /// Gets or sets category title
        /// </summary>
        [Display(Name ="عنوان")]
        public string Title { get; set; }
        /// <summary>
        ///  Gets or sets category description
        /// </summary>
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets category Parent identifier
        /// </summary>
        [Display(Name = "گروه پدر")]
        public string ParentId { get; set; }
    }
}

