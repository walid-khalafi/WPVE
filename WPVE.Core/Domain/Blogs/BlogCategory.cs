using System;
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
        public string Title { get; set; }
        /// <summary>
        ///  Gets or sets category description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets category Parent identifier
        /// </summary>
        public string ParentId { get; set; }
    }
}

