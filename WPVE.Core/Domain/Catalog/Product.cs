using System;
using System.ComponentModel.DataAnnotations;

namespace WPVE.Core.Domain.Catalog
{
    public class Product: BaseEntity
    {
        public Product()
        {
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [Display(Name="نام محصول")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the short description
        /// </summary>
        [Display(Name = "توضیحات کوتاه")]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the full description
        /// </summary>
        [Display(Name = "توضیحات محصول")]
        public string FullDescription { get; set; }

        /// <summary>
        /// Gets or sets the  product categories identifiers
        /// </summary>
        [Display(Name = "گروه محصول")]
        public string ProductCategories { get; set; }


        /// <summary>
        /// Gets or sets the product tags
        /// </summary>
        [Display(Name = "تگ های محصول")]
        public string ProductTags { get; set; }

        /// <summary>
        /// Gets or sets the Manufacturer  identifier
        /// </summary>
        [Display(Name = "شرکت سازنده")]
        public string ManufacturerId { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity
        /// </summary>
        [Display(Name = "موجودی انبار")]
        public int StockQuantity { get; set; }

        /// <summary>
        /// Gets or sets the order minimum quantity
        /// </summary>
        [Display(Name = "حداقل تعداد سفارش")]
        public int OrderMinimumQuantity { get; set; }
        //Order
        /// <summary>
        /// Gets or sets the order maximum quantity
        /// </summary>
        [Display(Name = "حداکثر تعداد سفارش")]
        public int OrderMaximumQuantity { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this item is available for Pre-Order
        /// </summary>
        [Display(Name = "فعال جهت ثبت سفارش پیش فروش")]
        public bool AvailableForPreOrder { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this product is returnable (a customer is allowed to submit return request with this product)
        /// </summary>
        [Display(Name = "غیر قابل بازگشت")]
        public bool NotReturnable { get; set; }
        //Sales
        /// <summary>
        /// Gets or sets the price
        /// </summary>
        [Display(Name = "قیمت")]
        public decimal Price { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to show "Call for Pricing" or "Call for quote" instead of price
        /// </summary>
        [Display(Name = "تماس برای قیمت")]
        public bool CallForPrice { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to disable buy (Add to cart) button
        /// </summary>
        [Display(Name = "حذف دکمه خرید")]
        public bool DisableBuyButton { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the product is marked as tax exempt
        /// </summary>
        [Display(Name = "معاف مالیاتی")]
        public bool IsTaxExempt { get; set; }
        /// <summary>
        /// Gets or sets the tax category identifier
        /// </summary>
        [Display(Name = "گروه مالیاتی")]
        public int TaxCategoryId { get; set; }


        //Shipping
        /// <summary>
        /// Gets or sets the weight
        /// </summary>
        [Display(Name = "وزن")]
        public decimal Weight { get; set; }

        /// <summary>
        /// Gets or sets the length
        /// </summary>
        [Display(Name = "طول")]
        public decimal Length { get; set; }

        /// <summary>
        /// Gets or sets the width
        /// </summary>
        [Display(Name = "عرض")]
        public decimal Width { get; set; }

        /// <summary>
        /// Gets or sets the height
        /// </summary>
        [Display(Name = "ارتفاع")]
        public decimal Height { get; set; }





        //Publish Settings
        /// <summary>
        /// Gets or sets a value indicating whether to show the product on home page
        /// </summary>
        [Display(Name = "نمایش در صفحه اصلی")]
        public bool ShowOnHomepage { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        [Display(Name = "انتشار محصول ")]
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        [Display(Name = "حذف شده")]
        public bool Deleted { get; set; }
        // SEO
        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        [Display(Name = "عنوان متا")]
        public string MetaTitle { get; set; }
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

        // multimedia
        /// <summary>
        /// Gets or sets the  intro video URL
        /// </summary>
        [Display(Name = "ویدیو معرفی")]
        public string IntoVideoUrl { get; set; }
        /// <summary>
        /// Gets or sets the  main picture URL
        /// </summary>
        [Display(Name = "تصویر شاخص")]
        public string MainPicUrl { get; set; }
        /// <summary>
        /// Gets or sets the all pictures URL
        /// </summary>
        [Display(Name = "تصاویر محصول")]
        public string Pictures { get; set; }

        /// <summary>
        /// Gets or sets the User identifier
        /// </summary>
        public string CreatedByUserID { get; set; }
    }
}

