using Microsoft.AspNetCore.Mvc;
using WebbApp.ViewModels;

namespace WebbApp.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    
    [HttpPost]
    public IActionResult SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {

        }

        return View(viewModel);
    }


}
