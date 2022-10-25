using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Floram.Models;
using Floram.Models.Entities;

namespace Floram.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Db_MarketContext _MarketContext;

    public HomeController(ILogger<HomeController> logger, Db_MarketContext dbContext)
    {
        _MarketContext = dbContext;
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<Barang> barangs = _MarketContext.Barangs.ToList();
        return View(barangs);
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
