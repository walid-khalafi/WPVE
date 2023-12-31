﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WPVE.Data;

#nullable disable

namespace WPVE.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230909190233_edit_intro_video_url_field")]
    partial class edit_intro_video_url_field
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WPVE.Core.Domain.Blogs.BlogCategory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ParentId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("blogCategories");
                });

            modelBuilder.Entity("WPVE.Core.Domain.Blogs.BlogComment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("BlogPostId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("BlogComments");
                });

            modelBuilder.Entity("WPVE.Core.Domain.Blogs.BlogPost", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("AllowComments")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("BlogPostCategoryId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("BodyOverview")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedByUserID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EndDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IncludeInSitemap")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("MetaDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MetaKeywords")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MetaTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("StartDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("WPVE.Core.Domain.Blogs.BlogSettings", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("AllowNotRegisteredUsersToLeaveComments")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("BlogCommentsMustBeApproved")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("CreatedByUserID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("NotifyAboutNewBlogComments")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("NumberOfTags")
                        .HasColumnType("int");

                    b.Property<int>("PostsPageSize")
                        .HasColumnType("int");

                    b.Property<bool>("ShowHeaderRssUrl")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("BlogSettings");
                });

            modelBuilder.Entity("WPVE.Core.Domain.Catalog.Manufacturer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MetaDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MetaKeywords")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MetaTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Published")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("WPVE.Core.Domain.Catalog.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("AvailableForPreOrder")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("CallForPrice")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("CreatedByUserID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("DisableBuyButton")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FullDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("IntroVideoUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsTaxExempt")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("Length")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("MainPicUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ManufacturerId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MetaDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MetaKeywords")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MetaTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("NotReturnable")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("OrderMaximumQuantity")
                        .HasColumnType("int");

                    b.Property<int>("OrderMinimumQuantity")
                        .HasColumnType("int");

                    b.Property<string>("Pictures")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ProductCategories")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProductTags")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Published")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("ShowOnHomepage")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.Property<int>("TaxCategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Width")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WPVE.Core.Domain.Catalog.ProductCategory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MetaDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MetaKeywords")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MetaTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Published")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("WPVE.Core.Domain.Catalog.ProductTag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("ProductTags");
                });

            modelBuilder.Entity("WPVE.Core.Domain.Users.Profile", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NavigationSize")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ThemeColor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
