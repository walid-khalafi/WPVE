﻿using System;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "نمایش در sitemap")]
        public bool IncludeInSitemap { get; set; }

        /// <summary>
        /// Gets or sets the blog post title
        /// </summary>
        [Display(Name ="عنوان")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the blog post body
        /// </summary>
        [Display(Name = "متن اصلی")]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the blog post overview. If specified, then it's used on the blog page instead of the "Body"
        /// </summary>
        [Display(Name = "خلاصه متن")]
        public string BodyOverview { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the blog post comments are allowed 
        /// </summary>
        [Display(Name = "اجازه ثبت نظر")]
        public bool AllowComments { get; set; }
        /// <summary>
        /// Gets or sets the blog tags
        /// </summary>
        [Display(Name = "تگ ها")]
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets the blog post start date and time
        /// </summary>
        [Display(Name = "شروع انتشار")]
        public DateTime? StartDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the blog post end date and time
        /// </summary>
        [Display(Name = "پایان انتشار")]
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

