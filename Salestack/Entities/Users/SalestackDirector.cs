using Salestack.Entities.Company;
using Salestack.Entities.Teams;
using Salestack.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salestack.Entities.Users;

public class SalestackDirector : SalestackBaseUser
{
    public SalestackDirector(
    Guid id,
    string name,
    string phoneNumber,
    CompanyOccupation occupation,
    Guid companyId,
    Authentication authentication) : base(id, name, phoneNumber, occupation, authentication)
    {
        CompanyId = companyId;
    }
    public SalestackCompany? Company { get; set; }

    [ForeignKey("Company")]
    public Guid CompanyId { get; set; }
    public List<SalestackTeam> Teams { get; set; } = [];

    public SalestackDirector()
    {
        
    }
}
