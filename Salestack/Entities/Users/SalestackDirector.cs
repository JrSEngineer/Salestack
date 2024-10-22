using Salestack.Entities.Company;
using Salestack.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salestack.Entities.Users;

public class SalestackDirector : SalestackBaseUser
{
    public SalestackDirector(
    Guid id,
    string name,
    string email,
    string phoneNumber,
    CompanyOccupation occupation,
    Guid companyId) : base(id, name, email, phoneNumber, occupation)
    {
        CompanyId = companyId;
    }
    public SalestackCompany? Company { get; set; }

    [ForeignKey("Company")]
    public Guid CompanyId { get; set; }

    public SalestackDirector()
    {
        
    }
}
