using Salestack.Entities.Company;
using Salestack.Entities.Teams;
using Salestack.Enums;
using System.ComponentModel.DataAnnotations;

namespace Salestack.Entities.Users;

public class SalestackManager : SalestackBaseUser
{
    public SalestackManager(
    Guid id,
    string name,
    string phoneNumber,
    CompanyOccupation occupation,
    Authentication authentication) : base(id, name, phoneNumber, occupation, authentication)
    {

    }

    [Required(ErrorMessage = "You must provide a valid VerificationCode."),
     Length(minimumLength: 8, maximumLength: 20)]
    public String VerificationCode { get; set; } = string.Empty;

    public SalestackCompany? Company { get; set; }

    public Guid CompanyId { get; set; }

    public List<SalestackTeam> Teams { get; set; } = [];

    public SalestackManager()
    {

    }
}
