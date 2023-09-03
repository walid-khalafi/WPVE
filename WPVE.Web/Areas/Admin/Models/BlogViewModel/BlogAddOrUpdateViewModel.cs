using System;
namespace WPVE.Web.Areas.Admin.Models.BlogViewModel
{
    public class BlogAddOrUpdateViewModel
    {
        public BlogAddOrUpdateViewModel()
        {
        }
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the blog post title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the blog post body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the blog post overview. If specified, then it's used on the blog page instead of the "Body"
        /// </summary>
        public string BodyOverview { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the blog post comments are allowed 
        /// </summary>
        public bool AllowComments { get; set; }
        /// <summary>
        /// Gets or sets the blog tags
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets the blog post start date and time
        /// </summary>
        public DateTime? StartDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the blog post end date and time
        /// </summary>
        public DateTime? EndDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }
        /// <summary>
        /// Get or sets the search engine friendly page name
        /// </summary>
        public string SeName { get; set; }
    }
}

