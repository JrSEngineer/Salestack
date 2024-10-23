using Salestack.Entities.Company;
using Salestack.Entities.Users;

namespace Salestack.Entities.Teams;

public class SalestackTeam
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public SalestackCompany? Company { get; set; }
    public Guid CompanyId { get; set; }
    public SalestackDirector? Director { get; set; }
    public Guid? DirectorId { get; set; }
    public SalestackManager? Manager { get; set; }
    public Guid? ManagerId { get; set; }
    public List<SalestackSeller> Sellers { get; set; } = [];
}
