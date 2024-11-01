using Salestack.Entities.Company;

namespace Salestack.Entities.SaleTargets;

public abstract class SalestackBaseSaleTarget
{
    protected SalestackBaseSaleTarget(Guid id, string name, string description, decimal price, Guid companyId)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        CompanyId = companyId;
    }

    protected SalestackBaseSaleTarget()
    {
        
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public SalestackCompany? Company { get; set; }
    public Guid CompanyId { get; set; }
}
