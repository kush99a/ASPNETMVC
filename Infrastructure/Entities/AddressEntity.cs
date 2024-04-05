namespace Infrastructure.Entities;

public class AddressEntity
{
	public string UserId { get; set; } = null!;
	public string StreetName { get; set; } = null!;
	public string PostalCode { get; set; } = null!;
	public string City { get; set; } = null!;
	public ICollection<UserEntity> Users { get; set; } = [];
}
