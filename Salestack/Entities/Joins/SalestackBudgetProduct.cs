using Salestack.Entities.SaleTargets;

namespace Salestack.Entities.Joins
{
    public class SalestackBudgetProduct
    {
        public Guid BudgetId { get; set; }

        public SalestackProduct? Product { get; set; } = null!;

        public Guid ProductId { get; set; }

        public int Amount { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
