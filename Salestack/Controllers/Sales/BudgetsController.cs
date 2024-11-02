using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities.Sales;

namespace Salestack.Controllers.Sales;

[Route("api/[controller]")]
[ApiController]
public class BudgetsController : ControllerBase
{
    private readonly SalestackDbContext _context;

    public BudgetsController(SalestackDbContext context)
    {
        _context = context;
    }

    [HttpPost("companyId={companyId}")]
    public async Task<IActionResult> CreateBudgetAsync(Guid companyId, SalestackBudget data)
    {
        var company = await _context.Company
            .AsNoTracking()
            .Include(c => c.Director)
            .Include(c => c.Managers)
            .Include(c => c.Sellers)
            .FirstOrDefaultAsync(c => c.Id == companyId);

        if (company == null)
        {
            return NotFound(new
            {
                Message = $"Company with id {companyId} not found."
            });
        }

        bool validUserToBudgetCreation = company.Director.Id == data.UserId ||
            company.Managers.Exists(m => m.Id == data.UserId) ||
            company.Sellers.Exists(s => s.Id == data.UserId);

        if (!validUserToBudgetCreation)
        {
            return BadRequest(new
            {
                Message = $"This company does not have any user with id {data.UserId}."
            });
        }

        var customer = await _context.Customer
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == data.CustomerId);

        if (customer == null)
        {
            return NotFound(new { Message = $"Customer with id {data.CustomerId} not found." });
        }

        Guid budgetId = Guid.NewGuid();

        foreach (var product in data.Products)
        {
            product.BudgetId = budgetId;
        }

        foreach (var service in data.Services)
        {
            service.BudgetId = budgetId;
        }

        var newBudget = new SalestackBudget
        {
            Id = budgetId,
            UserId = data.UserId,
            CustomerId = customer.Id,
            Products = data.Products,
            Services = data.Services,
        };

        await _context.Budget.AddAsync(newBudget);

        await _context.SaveChangesAsync();

        return Created("salestack-api", newBudget);
    }

    [HttpGet("customerId={customerId}")]
    public async Task<IActionResult> GetBudgetsByCustomerIdAsync(Guid customerId)
    {
        var budgets = await _context.Budget
            .Where(p => p.CustomerId == customerId)
            .ToListAsync();

        if (!budgets.Any())
        {
            return NotFound(new
            {
                Message = $"No budgets found for customer with id {customerId}."
            });
        }

        return Ok(budgets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBudgetByIdAsync(Guid id)
    {
        var budget = await _context.Budget
            .AsNoTracking()
            .Include(b => b.Products)
            .Include(b => b.Services)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (budget == null)
        {
            return NotFound(new
            {
                Message = $"Budget with id {id} not found."
            });
        }

        return Ok(budget);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateBudgetAsync(Guid id, SalestackBudget data)
    {
        var budget = await _context.Budget.FindAsync(id);

        if (budget == null)
        {
            return NotFound(new
            {
                Message = $"Budget with id {id} not found."
            });
        }

        budget.Status = data.Status;
        budget.Products = data.Products;
        budget.Services = data.Services;

        await _context.SaveChangesAsync();

        return Ok(budget);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBudgetAsync(Guid id)
    {
        var budget = await _context.Budget.FindAsync(id);

        if (budget == null)
        {
            return NotFound(new
            {
                Message = $"Budget with id {id} not found."
            });
        }

        _context.Budget.Remove(budget);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
