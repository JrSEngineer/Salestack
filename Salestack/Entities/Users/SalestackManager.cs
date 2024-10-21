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

    public String VerificationCode { get; set; }
}
