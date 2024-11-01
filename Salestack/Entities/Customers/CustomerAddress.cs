namespace Salestack.Entities.Customers;

public class CustomerAddress
{
    public Guid Id { get; set; }

    public string Street { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public int Number { get; set; }

    public string State { get; set; } = string.Empty;

    public string ZipCode { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public Guid CustomerId { get; set; }

    public CustomerAddress()
    {
        
    }
}
