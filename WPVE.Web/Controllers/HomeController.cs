﻿using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WPVE.Web.Models;

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
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

