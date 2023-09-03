using System;
namespace WPVE.Core.Domain.Blogs
{
    /// <summary>
    /// Represents a blog post
    /// </summary>
    public partial class BlogPost : BaseEntity
    {
        /// <summary>
        /// Gets or sets the language identifier
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether this blog post should be included in sitemap
        /// </summary>
        public bool IncludeInSitemap { get; set; }

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
        /// Gets or sets the User identifier
        /// </summary>
        public string CreatedByUserID { get; set; }
        /// <summary>
        /// Gets or sets Blog Post Category.
        /// </summary>
        public string BlogPostCategoryId { get; set; }
    }
}

