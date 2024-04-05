using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebbApp.ViewModels.AccountDetails;

public class AccountDetailsViewModel
{
    public ProfileInfoViewModel ProfileInfo { get; set; } = null!;
    public BasicInfoFormViewModel BasicInfoForm { get; set; } = null!;
    public AddressInfoFormViewModel AddressInfoForm { get; set; } = null!;
}
