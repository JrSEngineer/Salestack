using Salestack.Entities.Joins;
using Salestack.Enums;

namespace Salestack.Entities.Sales;

public class SalestackBudget
{
    public Guid Id { get; set; }

    public decimal TotalBudgetPrice { get; set; }

    public BudgetStatus Status { get; set; } = BudgetStatus.Waiting;

    public Guid UserId { get; set; }

    public Guid CustomerId { get; set; }

    public Guid? OrderId { get; set; }

    public List<SalestackBudgetProduct> Products { get; set; } = [];

    public List<SalestackBudgetService> Services { get; set; } = [];
}
