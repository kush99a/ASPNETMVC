using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebbApp.ViewModels.AccountDetails;

namespace WebbApp.Controllers;

public class AccountDetailsController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{
	private readonly UserManager<UserEntity> _userManager = userManager;
	private readonly SignInManager<UserEntity> _signInManager = signInManager;

	[HttpGet]
	[Route("/Account")]
    public async Task<IActionResult> Index()
	{
        var viewmodel = new AccountDetailsViewModel
        {
            BasicInfoForm = await PopulateBasicInfoFormAsync()
        };

        return View(viewmodel);
	}

    [HttpPost]
    public IActionResult SaveBasicInfo(AccountDetailsViewModel viewModel)
    {
        if (TryValidateModel(viewModel.BasicInfoForm)) 
        { 
            return RedirectToAction("Index","Home" );
        }

        return View("Index", viewModel);
    }

    [HttpPost]
    public IActionResult SaveAddressInfo(AccountDetailsViewModel viewModel)
    {
        if (TryValidateModel(viewModel.AddressInfoForm))
        {
            return RedirectToAction("Index", "Home");
        }

        return View("Index", viewModel);
    }



    private async Task<BasicInfoFormViewModel> PopulateBasicInfoFormAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            return new BasicInfoFormViewModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,

            };
        }

        return null!;
    }

}
