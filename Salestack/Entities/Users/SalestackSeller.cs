using Salestack.Entities.Company;
using Salestack.Entities.Teams;
using Salestack.Enums;
namespace Salestack.Entities.Users;

public class SalestackSeller : SalestackBaseUser
{
    public SalestackSeller(
        Guid id,
        string name,
        string phoneNumber,
        CompanyOccupation occupation,
        Authentication authentication) : base(id, name, phoneNumber, occupation, authentication)
    {

    }
    public SalestackCompany? Company { get; set; }

    public Guid CompanyId { get; set; }

    public SalestackTeam? Team { get; set; }

    public Guid TeamId { get; set; }

    public SalestackSeller()
    {

    }
}
