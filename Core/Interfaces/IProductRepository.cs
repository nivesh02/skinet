using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entity;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
       Task<Product> GetProductByIdAsync(int Id) ;
       Task<IReadOnlyList<Product>> GetProductsAsync();
       Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
       Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
    }     
}