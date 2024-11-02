
namespace Salestack.Entities.SaleTargets;

public class SalestackService : SalestackBaseSaleTarget
{
    public SalestackService(
        Guid id,
        string name,
        string description,
        decimal price,
        Guid companyId) : base(id, name, description, price, companyId)
    {

    }

    public SalestackService()
    {
        
    }
}
