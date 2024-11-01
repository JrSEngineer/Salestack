using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities.Customers;

namespace Salestack.Controllers.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly SalestackDbContext _context;

        public CustomersController(SalestackDbContext context)
        {
            _context = context;
        }

        [HttpPost("companyId={companyId}")]
        public async Task<IActionResult> CreateCustomerAsync(Guid companyId, SalestackCustomer data)
        {
            var company = await _context.Company
                .Include(c => c.Director)
                .FirstOrDefaultAsync(c => c.Id == companyId);

            if (company == null)
            {
                return NotFound(new
                {
                    Message = $"Company with id {companyId} not found."
                });
            }

            Guid customerId = Guid.NewGuid();

            var newCustomer = new SalestackCustomer
            {
                Id = customerId,
                Name = data.Name,
                PhoneNumber = data.PhoneNumber,
                Address = new CustomerAddress
                {
                    Id = Guid.NewGuid(),
                    Street = data.Address.Street,
                    Number = data.Address.Number,
                    City = data.Address.City,
                    State = data.Address.State,
                    Country = data.Address.Country,
                    ZipCode = data.Address.ZipCode,
                    CustomerId = customerId,
                },
                CompanyId = companyId
            };

            await _context.Customer.AddAsync(newCustomer);

            await _context.SaveChangesAsync();

            return Created("salestack-api", newCustomer);
        }

        [HttpGet("companyId={companyId}")]
        public async Task<IActionResult> GetAllCompanyCustomersAsync(Guid companyId)
        {
            var company = await _context.Company.FindAsync(companyId);

            if (company == null)
            {
                return NotFound(new

                {
                    Message = $"Company with id {companyId} not found."
                });
            }

            var customers = await _context.Customer.Where(c => c.CompanyId == companyId).ToListAsync();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _context.Customer
                .AsNoTracking()
                .Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound(new
                {
                    Message = $"Customer with id {id} not found."
                });
            }

            return Ok(customer);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCustomerAsync(Guid id, SalestackCustomer data)
        {
            var customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new
                {
                    Message = $"Customer with id {id} not found."
                });
            }

            customer.Name = data.Name;
            customer.PhoneNumber = data.PhoneNumber;

            customer.Address.Street = data.Address.Street;
            customer.Address.Number = data.Address.Number;
            customer.Address.City = data.Address.City;
            customer.Address.State = data.Address.State;
            customer.Address.Country = data.Address.Country;
            customer.Address.ZipCode = data.Address.ZipCode;


            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(Guid id)
        {
            var customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new
                {
                    Message = $"Customer with id {id} not found."
                });
            }

            _context.Customer.Remove(customer);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
