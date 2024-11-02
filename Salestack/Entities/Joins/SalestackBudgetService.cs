using Salestack.Entities.SaleTargets;

namespace Salestack.Entities.Joins
{
    public class SalestackBudgetService
    {
        public Guid BudgetId { get; set; }

        public SalestackService? Service { get; set; } = null!;

        public Guid ServiceId { get; set; }

        public int Amount { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
