using System;
namespace WPVE.Web.Models.HomeViewModel
{
    public class HeroViewModel
    {
        public HeroViewModel()
        {
        }
        /// <summary>
        /// Get or set Hero Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Get or set Heading
        /// </summary>
        public string Heading { get; set; }
        /// <summary>
        /// Get or set Description Paragraph
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Get or set Hero Picture example : ~/home/images/illustrator/Startup_SVG.svg
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// Get or set button 1 Title
        /// </summary>
        public string Button1Title { get; set; }
        /// <summary>
        ///  Get or set button 1 visible
        /// </summary>
        public bool Button1Visible { get; set; }
        /// <summary>
        ///  Get or set button 1 icon class
        /// </summary>
        public string Button1Icon { get; set; }
        /// <summary>
        ///  Get or set button 1 url 
        /// </summary>
        public string Button1Url { get; set; }
        /// <summary>
        ///   /// Get or set button 2 Title
        /// </summary>
        public string Button2Title { get; set; }
        /// <summary>
        ///  Get or set button 2 visible
        /// </summary>
        public bool Button2Visible { get; set; }
        /// <summary>
        ///  Get or set button 2 icon class
        /// </summary>
        public string Button2Icon { get; set; }
        /// <summary>
        ///   ///  Get or set button 2 url 
        /// </summary>
        public string Button2Url { get; set; }
    }
}

