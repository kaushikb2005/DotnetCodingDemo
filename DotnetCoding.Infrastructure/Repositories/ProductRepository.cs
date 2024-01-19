using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Core.Models.Dto;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        protected readonly DbContextClass _dbContext;
        public ProductRepository(DbContextClass dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetFilteredProduct(ProductSearchRequestDto requestModel)
        {
            var products = new List<Product>();

            var query = _dbContext.Products
                            .Where(p => p.IsActive)
                            .OrderByDescending(p => p.CreatedDate)
                            .AsQueryable();

            if(!string.IsNullOrEmpty(requestModel.ProductName))
            {
                query = query.Where(p => p.Name.ToLower().Contains(requestModel.ProductName.ToLower()));
            }
            if (requestModel.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= requestModel.MinPrice.Value);
            }
            if (requestModel.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= requestModel.MaxPrice.Value);
            }
            if (requestModel.StartDate.HasValue)
            {
                query = query.Where(p => p.CreatedDate >= requestModel.StartDate.Value);
            }
            if (requestModel.EndDate.HasValue)
            {
                query = query.Where(p => p.CreatedDate <= requestModel.EndDate.Value);
            }

            products = await query.ToListAsync();
            return products;
        }
    }
}
