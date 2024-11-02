using Salestack.Entities.SaleTargets;
using Salestack.Enums;

namespace Salestack.Entities.Sales;

public class SalestackOrder
{
    public Guid Id { get; set; }

    public List<SalestackProduct> Products { get; set; } = [];

    public List<SalestackService> Services { get; set; } = [];

    public Guid CustomerId { get; set; }

    public Guid BudgetId { get; set; }

    public OrderStatus Status { get; set; }

    public decimal TotalOrderPrice { get; set; }
}
