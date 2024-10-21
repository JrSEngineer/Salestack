using Salestack.Enums;

namespace Salestack.Entities;

public abstract class SalestackBaseUser
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public CompanyOccupation Occupation { get; set; }
}
