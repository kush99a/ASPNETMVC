using Infrastructure.Entities;

namespace WebbApp.ViewModels.AccountDetails;

public class AccountDetailsViewModel
{
    public UserEntity User { get; set; } = null!;
    public BasicInfoFormViewModel BasicInfoForm { get; set; } = null!;
    public AddressInfoFormViewModel AddressInfoForm { get; set; } = null!;
}
