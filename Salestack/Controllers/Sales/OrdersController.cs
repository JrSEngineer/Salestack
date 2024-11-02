using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities.Sales;
using Salestack.Enums;

namespace Salestack.Controllers.Sales;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{

    private readonly SalestackDbContext _context;

    public OrdersController(SalestackDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync(SalestackOrder data)
    {

        var order = await _context.Budget.FindAsync(data.BudgetId);

        if (order == null)
        {
            return NotFound(new
            {
                Message = $"Budget with id {data.BudgetId} not found."
            });
        }

        var newOrder = new SalestackOrder
        {
            Id = Guid.NewGuid(),
            Status = OrderStatus.Waiting,
            CustomerId = data.CustomerId,
            BudgetId = data.BudgetId,
            TotalOrderPrice = data.TotalOrderPrice
        };

        await _context.Order.AddAsync(newOrder);

        await _context.SaveChangesAsync();

        return Created("salestack-api", newOrder);
    }

    [HttpGet("customerId={customerId}")]
    public async Task<IActionResult> GetOrdersByCustomerIdAsync(Guid customerId)
    {
        var orders = await _context.Order
            .Where(p => p.CustomerId == customerId)
            .ToListAsync();

        if (!orders.Any())
        {
            return NotFound(new
            {
                Message = $"No orders found for customer with id {customerId}."
            });
        }

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderByIdAsync(Guid id)
    {
        var order = await _context.Order.FindAsync(id);

        if (order == null)
        {
            return NotFound(new
            {
                Message = $"Order with id {id} not found."
            });
        }

        var orderProducts = await _context.BudgetProduct
                                          .Include(bP => bP.Product)
                                          .Where(bP => bP.BudgetId == order.BudgetId)
                                          .ToListAsync();

        var orderServices = await _context.BudgetService
                                          .Include(bS => bS.Service)
                                          .Where(bS => bS.BudgetId == order.BudgetId)
                                          .ToListAsync();

        var budgetRelatedWithCurrentOrder = await _context.Budget
            .AsNoTracking()
            .Include(b => b.Products)
            .ThenInclude(bP => bP.Product)
            .Include(b => b.Services)
            .ThenInclude(bS => bS.Service)
            .FirstOrDefaultAsync(b => b.OrderId == order.Id);

        foreach (var orderProduct in orderProducts)
        {
            order.Products.Add(orderProduct.Product);
        }
        
        foreach (var orderService in orderServices)
        {
            order.Services.Add(orderService.Service);
        }

        return Ok(order);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateOrderAsync(Guid id, SalestackOrder data)
    {
        var order = await _context.Order.FindAsync(id);

        if (order == null)
        {
            return NotFound(new
            {
                Message = $"Order with id {id} not found."
            });
        }

        order.Status = data.Status;
        order.Products = data.Products;
        order.Services = data.Services;

        await _context.SaveChangesAsync();

        return Ok(order);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderAsync(Guid id)
    {
        var order = await _context.Order.FindAsync(id);

        if (order == null)
        {
            return NotFound(new
            {
                Message = $"Order with id {id} not found."
            });
        }

        _context.Order.Remove(order);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
