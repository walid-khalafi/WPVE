using System;
using WPVE.Core.Enums;
namespace WPVE.Web.Models.HomeViewModel
{
    public class SingleProductViewModel
    {
        public SingleProductViewModel()
        {
        }

        public SingleProductEnumeration Type { get; set; }
        public SingleProduct1 SingleProduct1 { get; set; }
        public SingleProduct2 SingleProduct2 { get; set; }
    }

    public class SingleProduct1 {

        public string Title { get; set; }
        public string Price { get; set; }
        public string ShortDescription { get; set; }
        public string ButtonLink { get; set; }
        public string ButtonLinkText { get; set; }
        public List<string> PictureUrls { get; set; }
    }

    public class SingleProduct2 {

        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string PicutrUrl { get; set; }
        public string Link { get; set; }
        public string LinkText { get; set; }
        public string Icon1 { get; set; }
        public string IconTitle1 { get; set; }
        public string IconDescription1 { get; set; }
        public string Icon2 { get; set; }
        public string IconTitle2 { get; set; }
        public string IconDescription2 { get; set; }
        public string Icon3 { get; set; }
        public string IconTitle3 { get; set; }
        public string IconDescription3 { get; set; }
        public string Icon4 { get; set; }
        public string IconTitle4 { get; set; }
        public string IconDescription4 { get; set; }


    }
}

