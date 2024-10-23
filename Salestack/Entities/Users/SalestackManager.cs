using Salestack.Entities.Company;
using Salestack.Enums;
using System.ComponentModel.DataAnnotations;

namespace Salestack.Entities.Users;

public class SalestackManager : SalestackBaseUser
{
    public SalestackManager(
    Guid id,
    string name,
    string email,
    string phoneNumber,
    CompanyOccupation occupation) : base(id, name, email, phoneNumber, occupation) { }

    [Required(ErrorMessage = "You must provide a valid VerificationCode."),
     Length(minimumLength: 8)]
    public String VerificationCode { get; set; } = string.Empty;

    public SalestackCompany? Company { get; set; }

    public Guid CompanyId { get; set; }

    public SalestackManager()
    {

    }
}
