
using Myapi.Models.product;
using MyApi.Data;

namespace MyApi.Services;

public class ProductService : ProductModel
{
    private readonly AppDbContext _context;
    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public List<ProductModel> GetAll()
    {

        return _context.Products.ToList();
    }

    public ProductModel GetById(int id)
    {
        ProductModel res = _context.Products.FirstOrDefault(p => p.Id == id);
        return res;
    }

    public ProductModel Create(ProductModel product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public ProductModel Update(int id, ProductModel product)
    {
        var existingProduct = _context.Products.FirstOrDefault(p => p.Id == id);
        if (existingProduct == null)
        {
            return null;
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Description = product.Description;

        _context.SaveChanges();
        return existingProduct;
    }
}