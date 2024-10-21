using Salestack.Enums;

namespace Salestack.Entities;

public abstract class SalestackBaseUser
{
    protected SalestackBaseUser(
        Guid id,
        string name,
        string email,
        string phoneNumber,
        CompanyOccupation occupation)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Occupation = occupation;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public CompanyOccupation Occupation { get; set; }
}
