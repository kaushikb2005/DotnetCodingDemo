using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Core.Models.Dto;
using DotnetCoding.Services.Interfaces;

namespace DotnetCoding.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var productDetailsList = await _unitOfWork.Products.GetAll();
            return productDetailsList;
        }

        public async Task<IEnumerable<Product>> GetFilteredProducts(ProductSearchRequestDto requestModel)
        {
            var productDetailsList = await _unitOfWork.Products.GetFilteredProduct(requestModel);
            return productDetailsList;
        }

        public async Task<Product> GetProductById(int productId)
        {
            if (productId > 0)
            {
                var productDetails = await _unitOfWork.Products.GetById(productId);
                if (productDetails != null)
                {
                    return productDetails;
                }
            }
            return null;
        }

        public async Task<int> CreateProduct(Product product)
        {
            if (product != null)
            {
                product.ApprovalStatus = Constants.ApprovalStatusApproved;
                product.IsActive = true;
                product.State = Constants.ProductStateCreate;
                // Check if the price is more than $5000, add the product to the approval queue
                if (product.Price > 5000)
                {
                    product.IsActive = false;
                    product.ApprovalStatus = Constants.ApprovalStatusPending;
                }
                await _unitOfWork.Products.Add(product);
                var result = _unitOfWork.Save();

                // If the price is more than $5000, add the product to the approval queue
                if (product.Price > 5000)
                {
                    CreateProductApprovalQueue(product.ProductId, Constants.ProductExceeds5000);
                }

                if (result > 0)
                {
                    return product.ProductId;
                }   
                else
                    return 0;
            }
            return 0;
        }

        public async Task<bool> UpdateProduct(Product productModel)
        {
            if (productModel != null)
            {
                var existingProduct = await _unitOfWork.Products.GetById(productModel.ProductId);
                if (existingProduct != null)
                {
                    // Check if the updated price is more than 50 % of the previous price
                    if (productModel.Price > existingProduct.Price * 1.5)
                    {
                        // If so, add the product to the approval queue
                        CreateProductApprovalQueue(existingProduct.ProductId, Constants.ProductIncreaseBy50Percent);
                        existingProduct.ApprovalStatus = Constants.ApprovalStatusPending;
                        existingProduct.IsActive = false;
                    }

                    existingProduct.Name = productModel.Name;
                    existingProduct.Description = productModel.Description;
                    existingProduct.Price = productModel.Price;
                    existingProduct.ApprovalStatus = existingProduct.ApprovalStatus;
                    existingProduct.ModifiedDate = DateTime.UtcNow;
                    existingProduct.State = Constants.ProductStateUpdate;
                    _unitOfWork.Products.Update(existingProduct);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            if (productId > 0)
            {
                var productDetails = await _unitOfWork.Products.GetById(productId);
                if (productDetails != null)
                {
                    productDetails.IsActive = false;
                    productDetails.State = Constants.ProductStateDelete;
                    productDetails.ApprovalStatus = Constants.ApprovalStatusPending;
                    _unitOfWork.Products.Update(productDetails);
                    var result = _unitOfWork.Save();
                    
                    if (result > 0)
                    {
                        CreateProductApprovalQueue(productDetails.ProductId, Constants.RequestReasonDelete);
                        //// Update product state immediately
                        //await UpdateProductState(productDetails.ProductId, "Delete");
                        return true;
                    }
                    else
                        return false;
                }
            }
            return false;
        }

        // Additional method to Add product in approval queue
        private async void CreateProductApprovalQueue(int productId, string requestReason)
        {
            if (productId > 0)
            {
                //var queueProducts = await _unitOfWork.ApprovalQueues.GetByProductId(productId);
                //if (queueProducts != null)
                //{
                //    foreach(var queueProduct in queueProducts)
                //    {
                //        _unitOfWork.ApprovalQueues.Delete(queueProduct);
                //        _unitOfWork.Save();
                //    }
                //}
                // Create an approval queue item for the request
                var approvalQueueItem = new ProductApprovalQueue
                {
                    ProductId = productId,
                    RequestReason = requestReason,
                    RequestDate = DateTime.UtcNow
                };

                // Add the approval queue item
                await _unitOfWork.ApprovalQueues.Add(approvalQueueItem);
                _unitOfWork.Save();
            }
        }
    }
}
