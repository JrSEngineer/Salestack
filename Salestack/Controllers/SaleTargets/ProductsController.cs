using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities.SaleTargets;

namespace Salestack.Controllers.SaleTargets
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly SalestackDbContext _context;

        public ProductsController(SalestackDbContext context)
        {
            _context = context;
        }

        [HttpPost("companyId={companyId}/stcreatorId={stcreatorId}")]
        public async Task<IActionResult> CreateProductAsync(Guid companyId, Guid stcreatorId, SalestackProduct data)
        {
            var company = await _context.Company
                .FirstOrDefaultAsync(c => c.Id == companyId);

            if (company == null)
            {
                return NotFound(new { Message = $"Company with id {companyId} not found." });
            }

            var newProduct = new SalestackProduct
            {
                Id = Guid.NewGuid(),
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ProductImage = data.ProductImage,
                CompanyId = companyId
            };

            await _context.Product.AddAsync(newProduct);

            await _context.SaveChangesAsync();

            return Created("salestack-api", newProduct);
        }

        [HttpGet("companyId={companyId}")]
        public async Task<IActionResult> GetProductsByCompanyAsync(Guid companyId)
        {
            var company = await _context.Company.FindAsync(companyId);

            if (company == null)
            {
                return NotFound(new
                {
                    Message = $"No Company with id {companyId}."
                });
            }

            var products = await _context.Product
                .Where(p => p.CompanyId == companyId)
                .ToListAsync();

            if (!products.Any())
            {
                return NotFound(new
                {
                    Message = $"No products found for company with id {companyId}."
                });
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    Message = $"Product with id {id} not found."
                });
            }

            return Ok(product);
        }

        [HttpPatch("companyId={companyId}/stcreatorId={stcreatorId}/{id}")]
        public async Task<IActionResult> UpdateProductAsync(Guid companyId, Guid stcreatorId, Guid id, SalestackProduct data)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    Message = $"Product with id {id} not found."
                });
            }

            var productCompanyForUpdateOperation = await _context.Company
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .Include(c => c.Director)
            .Include(c => c.Managers)
            .FirstOrDefaultAsync(c => c.Id == companyId);

            bool allowedProductUpdateCondition = productCompanyForUpdateOperation!.Director.Id == stcreatorId ||
                productCompanyForUpdateOperation.Managers.Exists(m => m.Id == stcreatorId);

            if (!allowedProductUpdateCondition)
                return NotFound(new
                {
                    Message = $"No creator with id {stcreatorId} found in your company."
                });

            product.Name = data.Name;
            product.Description = data.Description;
            product.Price = data.Price;
            product.ProductImage = data.ProductImage;

            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("companyId={companyId}/stcreatorId={stcreatorId}/{id}")]
        public async Task<IActionResult> DeleteProductAsync(Guid companyId, Guid stcreatorId, Guid id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    Message = $"Product with id {id} not found."
                });
            }

            var productCompanyForDeleteOperation = await _context.Company
           .IgnoreAutoIncludes()
           .AsNoTracking()
           .Include(c => c.Director)
           .Include(c => c.Managers)
           .FirstOrDefaultAsync(c => c.Id == companyId);

            bool allowedProductDeleteCondition = productCompanyForDeleteOperation!.Director.Id == stcreatorId ||
                productCompanyForDeleteOperation.Managers.Exists(m => m.Id == stcreatorId);

            if (!allowedProductDeleteCondition)
                return NotFound(new
                {
                    Message = $"No creator with id {stcreatorId} found in your company."
                });

            _context.Product.Remove(product);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
