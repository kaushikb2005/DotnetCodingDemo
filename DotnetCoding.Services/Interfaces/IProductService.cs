using DotnetCoding.Core.Models;
using DotnetCoding.Core.Models.Dto;

namespace DotnetCoding.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int productId);
        Task<int> CreateProduct(Product productDetails);
        Task<bool> UpdateProduct(Product productDetails);
        Task<bool> DeleteProduct(int productId);
        Task<IEnumerable<Product>> GetFilteredProducts(ProductSearchRequestDto requestModel);
    }
}