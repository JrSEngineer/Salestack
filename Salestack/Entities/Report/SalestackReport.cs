using Salestack.Entities.Sales;

namespace Salestack.Entities.Report;

public class SalestackReport
{
    public Guid Id { get; set; }

    public List<SalestackOrder> ConcludedSales { get; set; } = [];

    public double TotalSalesValue { get; set; }
}
