using Salestack.Entities.Company;
using Salestack.Entities.Teams;
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
    public SalestackCompany? Company { get; set; }

    public Guid CompanyId { get; set; }
    
    public SalestackTeam? Team { get; set; }

    public Guid TeamId { get; set; }

    public SalestackSeller()
    {
        
    }
}
