using Salestack.Enums;

namespace Salestack.Entities.Users;

public class SalestackSeller : SalestackBaseUser
{
    public SalestackSeller(
        Guid id,
        string name,
        string email,
        string phoneNumber,
        CompanyOccupation occupation) : base(id, name, email, phoneNumber, occupation) { }
}
