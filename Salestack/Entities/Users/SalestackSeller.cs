using Salestack.Entities.Company;
using Salestack.Enums;
using System.ComponentModel.DataAnnotations.Schema;

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

    [ForeignKey("Company")]
    public Guid CompanyId { get; set; }

    public SalestackSeller()
    {
        
    }
}
