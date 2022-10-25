using System;
using Microsoft.AspNetCore.Mvc;
using Floram.Models;
using Floram.Models.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Floram.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly Db_MarketContext _dbContext;
    private IEnumerable<Claim>? claims;

    public UserController(ILogger<UserController> logger, Db_MarketContext dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public IActionResult Login()
    {
        return View(new LoginRequest());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if(!ModelState.IsValid){
            return View(request);
        }
        var user = _dbContext.Users.FirstOrDefault(x => x.Username == request.Username && x.Password == request.Password);

        if(user == null){
            ViewBag.ErrorMessage = "Invalid Username or Password";
            return View(request);
        }
        if(user.Tipe != "Pembeli"){
            ViewBag.ErrorMessage = "You'r not admin or seller";
            return View(request);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username!),
            new Claim("fullName",user.Username!),
            new Claim(ClaimTypes.Role, user.Tipe!),
            new Claim("Id", user.Id.ToString()!),
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
        var authProperties = new AuthenticationProperties{

        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return RedirectToAction("Index", "Home");
    }


    // Register Account
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(RegisterRequest reg)
    {
        if(!ModelState.IsValid)
        {
            return View(reg);
        }
        var newUser = new Models.Entities.User
        {
            Username = reg.Username,
            Password = reg.Password,
            Tipe = reg.Tipe
        };
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        
        if (reg.Tipe == "Penjual"){
            var penjual = new Models.Entities.Penjual
        {
            IdUser = newUser.Id,
            Alamat = reg.Alamat,
            NamaToko = $"{reg.Nama} Store",
            IdUserNavigation = newUser
        };
            _dbContext.Penjuals.Add(penjual);
            _dbContext.SaveChanges();
        }
        if (reg.Tipe == "Pembeli"){
            var pembeli = new Models.Entities.Pembeli
        {
            IdUser = newUser.Id,
            Alamat = reg.Alamat,
            NoHp = reg.NoHp,
            Nama = $"{reg.Nama} Store",
            IdUserNavigation = newUser
        };
            _dbContext.Pembelis.Add(pembeli);
            _dbContext.SaveChanges();
        }
        return RedirectToAction("Login");
    }

    public async Task<IActionResult> Logout(){
        await HttpContext.SignOutAsync();

        return RedirectToAction("Login");
    }
}