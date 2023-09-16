using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WPVE.Web.Models;
using WPVE.Web.Models.HomeViewModel;
namespace WPVE.Web.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _hostingEnvironment;


    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment)
    {
        _logger = logger;
        _hostingEnvironment = hostingEnvironment;
    }

    public IActionResult Index()
    {
        var model = new IndexViewModel();

        model.heroViewModel = new HeroViewModel()
        {
            Type = Core.Enums.HeroEnumeration.DefaultHearo,
            DefaultHero = new DefaultHero()
            {
                Heading = "موفقیت اتفاقی نیست خلافیت شماست",
                Description = "کمپین خود را راه اندازی کنید و از تخصص ما در زمینه طراحی و مدیریت صفحه بوت استرپ v5 html تبدیل محور بهره مند شوید.",
                Button1Icon = "uil uil-envelope",
                Button1Title = "شروع کنید",
                Button1Url = "#",
                Button1Visible = true,
                Button2Icon = "uil uil-book-alt",
                Button2Title = "اسناد",
                Button2Url = "#",
                Button2Visible = true,
                Picture = "/home/images/illustrator/Startup_SVG.svg",
            }
        };

        model.singleProductViewModel = new SingleProductViewModel()
        {
            Type = Core.Enums.SingleProductEnumeration.SingleProduct1,
            SingleProduct1 = new SingleProduct1() {
                Title = "",
                ShortDescription = "",
                Price = "",
                ButtonLink = "",
                ButtonLinkText = "",
                PictureUrls = new List<string>(),
               }
        };
           
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Contact() {

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

