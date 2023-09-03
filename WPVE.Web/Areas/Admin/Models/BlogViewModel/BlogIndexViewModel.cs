using System;
namespace WPVE.Web.Areas.Admin.Models.BlogViewModel
{
    public  class BlogIndexViewModel
    {
        #region Ctor
        public BlogIndexViewModel()
        {
          
        }
        #endregion

        #region Properties
        public string Id { get; set; }
        public string Title { get; set; }

        public DateTime? StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public DateTime CreatedOn { get; set; }

        #endregion
    }
}

