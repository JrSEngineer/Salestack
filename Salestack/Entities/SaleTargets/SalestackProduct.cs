
namespace Salestack.Entities.SaleTargets;

public class SalestackProduct : SalestackBaseSaleTarget
{
    public SalestackProduct(
        Guid id,
        string name,
        string description,
        decimal price,
         Guid companyId) : base(id, name, description, price, companyId)
    {

    }

    public SalestackProduct()
    {
        
    }

    public string? ProductImage { get; set; }
}
