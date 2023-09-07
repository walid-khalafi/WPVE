using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WPVE.Core.Domain.Blogs;
using WPVE.Core.Domain.Users;
using WPVE.Core.Domain.Catalog;
namespace WPVE.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        #region Ctor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        #endregion
        /// <summary>
        /// DataSet of Blog Blog Categories
        /// </summary>
        public DbSet<BlogCategory> blogCategories { get; set; }
        /// <summary>
        /// DataSet of Blog Posts
        /// </summary>
        public DbSet<BlogPost> BlogPosts { get; set; }
        /// <summary>
        /// DataSet of Blog Settings
        /// </summary>
        public DbSet<BlogSettings> BlogSettings { get; set; }
        /// <summary>
        /// DataSet of Blog Comments
        /// </summary>
        public DbSet<BlogComment> BlogComments { get; set; }
        /// <summary>
        /// DataSet of Users Profile
        /// </summary>
        public DbSet<Profile>  Profiles { get; set; }

        /// <summary>
        /// DataSet of Products
        /// </summary>
        public DbSet<Product> Products { get; set; }
        /// <summary>
        /// DataSet of Product Categories
        /// </summary>
        public DbSet<ProductCategory> ProductCategories { get; set; }
        /// <summary>
        /// DataSet of Manufacturers
        /// </summary>
        public DbSet<Manufacturer> Manufacturers { get; set; }
        /// <summary>
        /// DataSet of Product Tags
        /// </summary>
        public DbSet<ProductTag> ProductTags { get; set; }

    }
}
