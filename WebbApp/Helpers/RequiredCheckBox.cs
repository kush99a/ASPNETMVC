using System.ComponentModel.DataAnnotations;

namespace WebbApp.Helpers;

public class RequiredCheckBox : ValidationAttribute
{
	public override bool IsValid(object? value) => value is bool b && b;
		
}
