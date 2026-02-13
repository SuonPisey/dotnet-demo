using Microsoft.AspNetCore.Mvc;
using Myapi.Models.product;
using MyApi.Services;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    [HttpGet]
    public List<ProductModel> GetAllProducts()
    {
        var productService = new ProductService();
        return productService.GetAll();
    }

}