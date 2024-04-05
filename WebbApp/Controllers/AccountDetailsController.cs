using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebbApp.ViewModels.AccountDetails;

namespace WebbApp.Controllers;

public class AccountDetailsController(UserManager<UserEntity> userManager, AddressManager addressManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AddressManager _addressManager = addressManager;

    [HttpGet]
    [Route("/Account")]
    public async Task<IActionResult> Index()
    {
        var viewModel = new AccountDetailsViewModel();
        viewModel.ProfileInfo = await PopulateProfilInfoAsync();
        viewModel.BasicInfoForm ??= await PopulateBasicInfoAsync();
        viewModel.AddressInfoForm ??= await PopulateAddressInfoAsync();

        return View(viewModel);
    }


    [HttpPost]
    [Route("/Account")]
    public async Task<IActionResult> Index(AccountDetailsViewModel viewModel)
    {
        if (viewModel.BasicInfoForm != null)
        {
            if (viewModel.BasicInfoForm.FirstName != null && viewModel.BasicInfoForm.LastName != null && viewModel.BasicInfoForm.Email != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.FirstName = viewModel.BasicInfoForm.FirstName;
                    user.LastName = viewModel.BasicInfoForm.LastName;
                    user.Email = viewModel.BasicInfoForm.Email;
                    user.PhoneNumber = viewModel.BasicInfoForm.PhoneNumber;

                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("IncorrectValues", "Something went wrong! Unable to save data.");
                        ViewData["ErrorMessage"] = "Something went wrong! Unable to save data.";

                    }
                }
            }
        }

        if (viewModel.AddressInfoForm != null)
        {
            if (viewModel.AddressInfoForm.AddressLine_1 != null && viewModel.AddressInfoForm.PostalCode != null && viewModel.AddressInfoForm.City != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var address = await _addressManager.GetAddressAsync(user.Id);
                    if (address != null)
                    {
                        address.StreetName =  viewModel.AddressInfoForm.AddressLine_1;
                        address.PostalCode = viewModel.AddressInfoForm.PostalCode;
                        address.City = viewModel.AddressInfoForm.City;

                        var result = await _addressManager.UpdateAddressAsync(address);
                        if (!result)
                        {
                            ModelState.AddModelError("IncorrectValues", "Something went wrong! Unable to save data.");
                            ViewData["ErrorMessage"] = "Something went wrong! Unable to save update address information.";
                        }

                    }
                    else
                    {
                        address = new AddressEntity
                        {
                            UserId = user.Id,
                            StreetName = viewModel.AddressInfoForm.AddressLine_1,
                            PostalCode = viewModel.AddressInfoForm.PostalCode,
                            City = viewModel.AddressInfoForm.City
                        };

                        var result = await _addressManager.CreateAddressAsync(address);
                        if (!result)
                        {
                            ModelState.AddModelError("IncorrectValues", "Something went wrong! Unable to save data.");
                            ViewData["ErrorMessage"] = "Something went wrong! Unable to save update address information.";
                        }
                    }
                }
            }
        }

        viewModel.ProfileInfo = await PopulateProfilInfoAsync();
        viewModel.BasicInfoForm ??= await PopulateBasicInfoAsync();
        viewModel.AddressInfoForm ??= await PopulateAddressInfoAsync();

        return View(viewModel);
    }

    private async Task<ProfileInfoViewModel> PopulateProfilInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new ProfileInfoViewModel
        {

            FirstName = user!.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
        };


    }



    private async Task<BasicInfoFormViewModel> PopulateBasicInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new BasicInfoFormViewModel
        {
            UserId = user!.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,

        };


    }

    private async Task<AddressInfoFormViewModel> PopulateAddressInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var address = await _addressManager.GetAddressAsync(user.Id);
            return new AddressInfoFormViewModel
            {
                AddressLine_1 = address.StreetName,
                PostalCode = address.PostalCode,
                City = address.City
            };
        }
        
        return new AddressInfoFormViewModel();


    }

}
