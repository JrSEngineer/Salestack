using Salestack.Enums;

namespace Salestack.Entities.Users;

public class SalestackDirector : SalestackBaseUser
{
    public SalestackDirector(
    Guid id,
    string name,
    string email,
    string phoneNumber,
    CompanyOccupation occupation) : base(id, name, email, phoneNumber, occupation) { }
}
