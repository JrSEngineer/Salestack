using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities.Customers;

namespace Salestack.Controllers.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsController : ControllerBase
    {

        private readonly SalestackDbContext _context;

        public VisitsController(SalestackDbContext context)
        {
            _context = context;
        }

        [HttpPost("{companyId}")]
        public async Task<IActionResult> CreateVisitAsync(Guid companyId, PresencialVisit data)
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

            bool validUserToVisitCreation = company.Director.Id == data.UserId ||
            company.Managers.Exists(m => m.Id == data.UserId) ||
            company.Sellers.Exists(s => s.Id == data.UserId);

            if (!validUserToVisitCreation)
            {
                return BadRequest(new
                {
                    Message = $"This company does not have any user with id {data.UserId}."
                });
            }

            var customerToBeVisited = await _context.Customer
                .IgnoreAutoIncludes()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == data.CustomerId);

            if (customerToBeVisited == null)
                return BadRequest(new
                {
                    Message = $"No customer found with id '{data.CustomerId}'."
                });

            var newVisit = new PresencialVisit
            {
                Id = Guid.NewGuid(),
                VisitDate = data.VisitDate,
                UserId = data.UserId,
                CustomerId = data.CustomerId
            };

            await _context.Visit.AddAsync(newVisit);

            await _context.SaveChangesAsync();

            return Created("salestack-api", newVisit);
        }

        [HttpGet("customerId={customerId}")]
        public async Task<IActionResult> GetAllCustomerVisitsAsync(Guid customerId)
        {
            var customerVisits = await _context.Visit.AsNoTracking()
                                                     .Where(v => v.CustomerId == customerId)
                                                     .ToListAsync();

            return Ok(customerVisits);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisitByIdAsync(Guid id)
        {
            var selectedVisit = await _context.Visit
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (selectedVisit == null)
                return NotFound(new
                {
                    Message = $"Visit with id {id} not found."
                });

            return Ok(selectedVisit);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateVisitAsync(Guid id, PresencialVisit data)
        {
            var selectedVisit = await _context.Visit.FindAsync(id);

            if (selectedVisit == null)
                return NotFound(new
                {
                    Message = $"Visit with id {id} not found."
                });

            selectedVisit.StartedAt = data.StartedAt;
            selectedVisit.FinishedAt = data.FinishedAt;
            selectedVisit.Status = data.Status;

            await _context.SaveChangesAsync();

            return Ok(selectedVisit);
        }

        [HttpDelete("companyId={companyId}/leaderId={leaderId}/{id}")]
        public async Task<IActionResult> DeleteVisitAsync(Guid companyId, Guid leaderId, Guid id)
        {
            var company = await _context.Company
          .AsNoTracking()
          .Include(c => c.Director)
          .Include(c => c.Managers)
          .FirstOrDefaultAsync(c => c.Id == companyId);

            if (company == null)
            {
                return NotFound(new
                {
                    Message = $"Company with id {companyId} not found."
                });
            }

            bool validUserToVisitDeleteProccess = company.Director.Id == leaderId ||
            company.Managers.Exists(m => m.Id == leaderId);

            if (!validUserToVisitDeleteProccess)
            {
                return BadRequest(new
                {
                    Message = $"This company does not have any user with id {leaderId}."
                });
            }

            var selectedVisit = await _context.Visit.FindAsync(id);

            if (selectedVisit == null)
                return NotFound(new
                {
                    Message = $"Visit with id {id} not found."
                });

            _context.Visit.Remove(selectedVisit);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
