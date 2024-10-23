using Salestack.Enums;
using System.ComponentModel.DataAnnotations;

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

    protected SalestackBaseUser()
    {

    }

    public Guid Id { get; set; }

    [Required(ErrorMessage = "You must provide a valid Name.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "You must provide a valid Email.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "You must provide a valid PhoneNumber."),
     Length(minimumLength:8, maximumLength: 14)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "You must provide a valid Occupation.")]
    public CompanyOccupation Occupation { get; set; }
}
