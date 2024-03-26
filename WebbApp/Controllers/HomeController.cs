using Microsoft.AspNetCore.Mvc;

namespace WebbApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Home";
        return View();
    }
}
