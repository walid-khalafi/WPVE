using System;
using WPVE.Core;
using WPVE.Core.Domain.Blogs;
using WPVE.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WPVE.Services.Blogs
{
    public class BlogService : IBlogService
    {
        #region Fields
        private readonly ApplicationDbContext _db;
        #endregion

        #region Ctor
        public BlogService(ApplicationDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Methods

        #region Blog posts
        /// <summary>
        /// Get a value indicating whether a blog post is available now (availability dates)
        /// </summary>
        /// <param name="blogPost"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public bool BlogPostIsAvailable(BlogPost blogPost, DateTime? dateTime = null)
        {
            if (blogPost == null)
                throw new ArgumentNullException(nameof(blogPost));

            if (blogPost.StartDateUtc.HasValue && blogPost.StartDateUtc.Value >= (dateTime ?? DateTime.UtcNow))
                return false;

            if (blogPost.EndDateUtc.HasValue && blogPost.EndDateUtc.Value <= (dateTime ?? DateTime.UtcNow))
                return false;

            return true;
        }
        /// <summary>
        ///  Deletes a blog post
        /// </summary>
        /// <param name="blogPost"></param>
        /// <returns></returns>
        public async Task DeleteBlogPostAsync(string blogPostId)
        {
            if (string.IsNullOrWhiteSpace(blogPostId))
            {
                return;
            }
            var blogPost = await _db.BlogPosts.FindAsync(blogPostId);
            if (blogPost !=  null)
            {
                _db.BlogPosts.Remove(blogPost);
                await _db.SaveChangesAsync();
            }
            return;
        }
        /// <summary>
        /// Gets all blog posts
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="showHidden"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<IPagedList<BlogPost>> GetAllBlogPostsAsync(int languageId = 0, DateTime? dateFrom = null, DateTime? dateTo = null, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, string title = null)
        {
         var data = await Task.FromResult(_db.BlogPosts.ToList());
            if (dateFrom.HasValue)
                data = data.Where(b => dateFrom.Value <= (b.StartDateUtc ?? b.CreatedOnUtc)).ToList();

            if (dateTo.HasValue)
                data = data.Where(b => dateTo.Value >= (b.StartDateUtc ?? b.CreatedOnUtc)).ToList();

            if (languageId > 0)
                data = data.Where(b => languageId == b.LanguageId).ToList();

            if (!string.IsNullOrEmpty(title))
                data = data.Where(b => b.Title.Contains(title)).ToList();

            if (!showHidden)
            {
                data = data.Where(b => !b.StartDateUtc.HasValue || b.StartDateUtc <= DateTime.UtcNow).ToList();
                data = data.Where(b => !b.EndDateUtc.HasValue || b.EndDateUtc >= DateTime.UtcNow).ToList();
            }

            data = data.OrderByDescending(b => b.StartDateUtc ?? b.CreatedOnUtc).ToList();

           var result = new PagedList<BlogPost>(data, pageIndex, pageSize);
            return result;
            }
        /// <summary>
        /// Gets all blog posts
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="tag"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="showHidden"></param>
        /// <returns></returns>
        public async Task<IPagedList<BlogPost>> GetAllBlogPostsByTagAsync( int languageId = 0, string tag = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            tag = tag.Trim();

            //we load all records and only then filter them by tag
            var blogPostsAll = await GetAllBlogPostsAsync(languageId: languageId, showHidden: showHidden);
            var taggedBlogPosts = new List<BlogPost>();
            foreach (var blogPost in blogPostsAll)
            {
                var tags = await ParseTagsAsync(blogPost);
                if (!string.IsNullOrEmpty(tags.FirstOrDefault(t => t.Equals(tag, StringComparison.InvariantCultureIgnoreCase))))
                    taggedBlogPosts.Add(blogPost);
            }

            //server-side paging
            var result = new PagedList<BlogPost>(taggedBlogPosts, pageIndex, pageSize);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blogPostId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<BlogPost> GetBlogPostByIdAsync(string blogPostId)
        {
            return await _db.BlogPosts.FindAsync(blogPostId);
        }
        /// <summary>
        /// Returns all posts published between the two dates.
        /// </summary>
        /// <param name="blogPosts"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public async Task<IList<BlogPost>> GetPostsByDateAsync(IList<BlogPost> blogPosts, DateTime dateFrom, DateTime dateTo)
        {
            if (blogPosts == null)
                throw new ArgumentNullException(nameof(blogPosts));

            var result = await Task.FromResult( blogPosts
                .Where(p => dateFrom.Date <= (p.StartDateUtc ?? p.CreatedOnUtc) && (p.StartDateUtc ?? p.CreatedOnUtc).Date <= dateTo)
                .ToList());

            return result;
        }
        /// <summary>
        /// Inserts a blog post
        /// </summary>
        /// <param name="blogPost"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task InsertBlogPostAsync(BlogPost blogPost)
        {
            if (blogPost == null)
            {
                Console.WriteLine("blogPost is Null");
                return;
            }
            try
            {
                _db.BlogPosts.Add(blogPost);
                await _db.SaveChangesAsync();

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Updates the blog post
        /// </summary>
        /// <param name="blogPost"></param>
        /// <returns></returns>
        public async Task UpdateBlogPostAsync(BlogPost blogPost)
        {
            var data = await _db.BlogPosts.FindAsync(blogPost.Id);
            if (data == null)
            {
                Console.WriteLine("blogPost NotFound!");
                return;
            }
            data.LanguageId = blogPost.LanguageId;
            data.Title = blogPost.Title;
            data.Body = blogPost.Body;
            data.AllowComments = blogPost.AllowComments;
            data.BodyOverview = blogPost.BodyOverview;
            data.CreatedOnUtc = blogPost.CreatedOnUtc;
            data.StartDateUtc = blogPost.StartDateUtc;
            data.EndDateUtc = blogPost.EndDateUtc;
            data.IncludeInSitemap = blogPost.IncludeInSitemap;
            data.MetaDescription = blogPost.MetaDescription;
            data.MetaKeywords = blogPost.MetaKeywords;
            data.MetaTitle = blogPost.MetaTitle;
            data.Tags = blogPost.Tags;
            data.IPAddress = blogPost.IPAddress;
            data.CreatedByUserID = blogPost.CreatedByUserID;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
         
        }
        /// <summary>
        /// Parse tags
        /// </summary>
        /// <param name="blogPost"></param>
        /// <returns></returns>
        public async Task<IList<string>> ParseTagsAsync(BlogPost blogPost)
        {
            if (blogPost == null)
                throw new ArgumentNullException(nameof(blogPost));

            if (blogPost.Tags == null)
                return new List<string>();

            var tags = await Task.FromResult( blogPost.Tags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(tag => tag.Trim())
                .Where(tag => !string.IsNullOrEmpty(tag))
                .ToList());

            return tags;
        }

        #endregion

        #region Blog Comments
        /// <summary>
        /// Gets all comments
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="blogPostId"></param>
        /// <param name="approved"></param>
        /// <param name="fromUtc"></param>
        /// <param name="toUtc"></param>
        /// <param name="commentText"></param>
        /// <returns></returns>
        public async Task<IList<BlogComment>> GetAllCommentsAsync(int customerId = 0, string? blogPostId = null, bool? approved = null, DateTime? fromUtc = null, DateTime? toUtc = null, string commentText = null)
        {
            var data = await Task.FromResult(_db.BlogComments.ToList());

            if (approved.HasValue)
                data = data.Where(comment => comment.IsApproved == approved).ToList();

            if (!string.IsNullOrWhiteSpace(blogPostId))
                data = data.Where(comment => comment.BlogPostId == blogPostId).ToList();

            if (customerId > 0)
                data = data.Where(comment => comment.CustomerId == customerId).ToList();

            if (fromUtc.HasValue)
                data = data.Where(comment => fromUtc.Value <= comment.CreatedOnUtc).ToList();

            if (toUtc.HasValue)
                data = data.Where(comment => toUtc.Value >= comment.CreatedOnUtc).ToList();

            if (!string.IsNullOrEmpty(commentText))
                data = data.Where(c => c.CommentText.Contains(commentText)).ToList();

            data = data.OrderBy(comment => comment.CreatedOnUtc).ToList();

            return data;
        }
        /// <summary>
        /// Gets a blog comment
        /// </summary>
        /// <param name="blogCommentId"></param>
        /// <returns></returns>
        public async Task<BlogComment> GetBlogCommentByIdAsync(int blogCommentId)
        {
            return await _db.BlogComments.FindAsync(blogCommentId);
        }
        /// <summary>
        /// Get the count of blog comments
        /// </summary>
        /// <param name="blogPost"></param>
        /// <param name="isApproved"></param>
        /// <returns></returns>
        public async Task<int> GetBlogCommentsCountAsync(BlogPost blogPost, bool? isApproved = null)
        {
            var query = _db.BlogComments.Where(comment => comment.BlogPostId == blogPost.Id).ToList();

            if (isApproved.HasValue)
                query = query.Where(comment => comment.IsApproved == isApproved.Value).ToList();

            return query.Count;
        }
        /// <summary>
        /// Insert a blog comment
        /// </summary>
        /// <param name="blogComment"></param>
        /// <returns></returns>
        public async Task InsertBlogCommentAsync(BlogComment blogComment)
        {
            if (blogComment == null)
            {
                Console.WriteLine("BlogComments is Null");
                return;
            }
            try
            {
                _db.BlogComments.Add(blogComment);
                await _db.SaveChangesAsync();

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Delete a blog comment
        /// </summary>
        /// <param name="blogCommentId"></param>
        /// <returns></returns>
        public async Task DeleteBlogCommentAsync(string blogCommentId)
        {
            if (string.IsNullOrWhiteSpace(blogCommentId))
            {
                return;
            }
            var blogComment = await _db.BlogComments.FindAsync(blogCommentId);
            if (blogComment != null)
            {
                _db.BlogComments.Remove(blogComment);
                await _db.SaveChangesAsync();
            }
            return;
        }
        /// <summary>
        /// Deletes blog comments
        /// </summary>
        /// <param name="blogComments"></param>
        /// <returns></returns>
        public async Task DeleteBlogCommentsAsync(IList<BlogComment> blogComments)
        {
            foreach (var item in blogComments)
            {
                await DeleteBlogCommentAsync(item.Id);
            }
        }
        /// <summary>
        /// Update a blog comment
        /// </summary>
        /// <param name="blogComment"></param>
        /// <returns></returns>
        public async Task UpdateBlogCommentAsync(BlogComment blogComment)
        {
            var data = await _db.BlogComments.FindAsync(blogComment.Id);
            if (data == null)
            {
                Console.WriteLine("blogComment NotFound!");
                return;
            }

            data.CommentText = blogComment.CommentText;
            data.CreatedOnUtc = blogComment.CreatedOnUtc;
            data.IsApproved = blogComment.IsApproved;
            data.IPAddress = blogComment.IPAddress;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        #endregion

        #endregion
    }
}

