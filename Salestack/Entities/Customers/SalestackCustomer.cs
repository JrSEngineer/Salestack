namespace Salestack.Entities.Customers;

public class SalestackCustomer
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public CustomerAddress Address { get; set; } = null!;

    public Guid AddressId { get; set; }

    public Guid CompanyId { get; set; }

    public SalestackCustomer()
    {
        
    }
}
