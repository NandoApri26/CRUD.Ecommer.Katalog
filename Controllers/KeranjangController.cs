using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Floram.Models;
using Floram.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Floram.Controllers;

public class KeranjangController : Controller
{
    private readonly ILogger<KeranjangController> _logger;
    private readonly Db_MarketContext _dbContext;

    public KeranjangController(ILogger<KeranjangController> logger, Db_MarketContext db_Market)
    {
        _dbContext = db_Market;
        _logger = logger;
    }

    public IActionResult AddKeranjang (int id)
    {
        Barang br = _dbContext.Barangs.First(x => x.Id == id);
        int userId = int.Parse(User.Claims.First(x => x.Type == "Id").Value);
        Keranjang kr = new Keranjang{
            IdBarang = id,
            IdUser = userId,
            HargaSatuan = br.Harga,
            Jumlah = 1
        };
        _dbContext.Keranjangs.Add(kr);
        _dbContext.SaveChanges();
        return RedirectToAction("Index", "Keranjang");
    }

    public IActionResult Index ()
    {
        int dataUser = int.Parse(User.Claims.First(x => x.Type == "Id").Value);
        List<Keranjang> keranjangs = _dbContext.Keranjangs.Include(x => x.IdBarangNavigation).ThenInclude(x => x.IdPenjualNavigation).Include(x => x.IdUserNavigation).Where(x => x.IdUser == dataUser).ToList();
        return View(keranjangs);
    }
}