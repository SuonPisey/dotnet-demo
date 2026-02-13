
using Myapi.Models.product;

namespace MyApi.Services;

public class ProductService : ProductModel
{
    private static List<ProductModel> _products = new()
    {
        new ProductModel { Id = 1, Name = "Laptop", Price = 1200 },
        new ProductModel { Id = 2, Name = "Mouse", Price = 25 }
    };

    public List<ProductModel> GetAll()
    {
        return _products;
    }
}