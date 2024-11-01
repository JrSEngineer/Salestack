using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities.SaleTargets;

namespace Salestack.Controllers.SaleTargets
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {

        private readonly SalestackDbContext _context;

        public ServicesController(SalestackDbContext context)
        {
            _context = context;
        }

        [HttpPost("companyId={companyId}/stcreatorId={stcreatorId}")]
        public async Task<IActionResult> CreateServiceAsync(Guid companyId, Guid stcreatorId, SalestackService data)
        {
            var company = await _context.Company
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == companyId);

            if (company == null)
            {
                return NotFound(new { Message = $"Company with id {companyId} not found." });
            }

            var newService = new SalestackService
            {
                Id = Guid.NewGuid(),
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                CompanyId = companyId
            };

            await _context.Service.AddAsync(newService);

            await _context.SaveChangesAsync();

            return Created("salestack-api", newService);
        }

        [HttpGet("companyId={companyId}")]
        public async Task<IActionResult> GetServicesByCompanyAsync(Guid companyId)
        {
            var services = await _context.Service
                .Where(s => s.CompanyId == companyId)
                .ToListAsync();

            if (!services.Any())
            {
                return NotFound(new
                {
                    Message = $"No services found for company with id {companyId}."

                });
            }

            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceByIdAsync(Guid id)
        {
            var service = await _context.Service.FindAsync(id);

            if (service == null)
            {
                return NotFound(new
                {
                    Message = $"Service with id {id} not found."
                });
            }

            return Ok(service);
        }


        [HttpPatch("companyId={companyId}/stcreatorId={stcreatorId}/{id}")]
        public async Task<IActionResult> UpdateServiceAsync(Guid companyId, Guid stcreatorId, Guid id, SalestackService data)
        {
            var product = await _context.Service.FindAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    Message = $"Service with id {id} not found."
                });
            }

            var productCompanyForUpdateOperation = await _context.Company
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .Include(c => c.Director)
            .Include(c => c.Managers)
            .FirstOrDefaultAsync(c => c.Id == companyId);

            bool allowedServiceUpdateCondition = productCompanyForUpdateOperation!.Director.Id == stcreatorId ||
                productCompanyForUpdateOperation.Managers.Exists(m => m.Id == stcreatorId);

            if (!allowedServiceUpdateCondition)
                return NotFound(new
                {
                    Message = $"No creator with id {stcreatorId} found in your company."
                });

            product.Name = data.Name;
            product.Description = data.Description;
            product.Price = data.Price;

            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("companyId={companyId}/stcreatorId={stcreatorId}/{id}")]
        public async Task<IActionResult> DeleteServiceAsync(Guid companyId, Guid stcreatorId, Guid id)
        {
            var product = await _context.Service.FindAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    Message = $"Service with id {id} not found."
                });
            }

            var productCompanyForDeleteOperation = await _context.Company
           .IgnoreAutoIncludes()
           .AsNoTracking()
           .Include(c => c.Director)
           .Include(c => c.Managers)
           .FirstOrDefaultAsync(c => c.Id == companyId);

            bool allowedServiceDeleteCondition = productCompanyForDeleteOperation!.Director.Id == stcreatorId ||
                productCompanyForDeleteOperation.Managers.Exists(m => m.Id == stcreatorId);

            if (!allowedServiceDeleteCondition)
                return NotFound(new
                {
                    Message = $"No creator with id {stcreatorId} found in your company."
                });

            _context.Service.Remove(product);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
