using Salestack.Enums;
using System.ComponentModel.DataAnnotations;

namespace Salestack.Entities.Users;

public abstract class SalestackBaseUser
{
    protected SalestackBaseUser(
        Guid id,
        string name,
        string phoneNumber,
        CompanyOccupation occupation,
        Authentication authentication)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        Occupation = occupation;
        Authentication = authentication;
    }

    protected SalestackBaseUser()
    {

    }

    public Guid Id { get; set; }

    [Required(ErrorMessage = "You must provide a valid Name.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "You must provide a valid PhoneNumber."),
     Length(minimumLength: 8, maximumLength: 14)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "You must provide a valid Occupation.")]
    public CompanyOccupation Occupation { get; set; }

    [Required(ErrorMessage = "You must provide valid authentication values.")]
    public Authentication Authentication { get; set; }

    public UserHierarchy Hierarchy { get; set; }
}
