using DotnetCoding.Core.Models;
using DotnetCoding.Core.Models.Dto;

namespace DotnetCoding.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<IEnumerable<Product>> GetFilteredProduct(ProductSearchRequestDto requestModel);
    }
}