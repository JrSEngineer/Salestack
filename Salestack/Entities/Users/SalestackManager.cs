using Salestack.Entities.Company;
using Salestack.Enums;

namespace Salestack.Entities.Users;

public class SalestackManager : SalestackBaseUser
{
    public SalestackManager(
    Guid id,
    string name,
    string email,
    string phoneNumber,
    CompanyOccupation occupation) : base(id, name, email, phoneNumber, occupation) { }

    public String VerificationCode { get; set; } = string.Empty;

    public SalestackCompany? Company { get; set; }

    public Guid CompanyId { get; set; }

    public SalestackManager()
    {

    }
}
