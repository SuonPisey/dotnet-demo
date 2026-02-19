using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myapi.Models.product;
using MyApi.Data;
using MyApi.Models;
using MyApi.Services;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("all")]
    public async Task<ActionResult<ApiResponse<List<ProductModel>>>> GetAllProducts()
    {
        var productService = new ProductService(_context);

        var products = productService.GetAll();

        var response = new ApiResponse<List<ProductModel>>(products, "Products retrieved successfully");

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ProductModel>>> GetProductById(int id)
    {
        var productService = new ProductService(_context);

        var product = productService.GetById(id);

        if (product == null)
        {
            return NotFound(new ApiResponse<ProductModel>(null, "Product not found"));
        }

        var response = new ApiResponse<ProductModel>(product, "Product retrieved successfully");

        return Ok(response);
    }

    [HttpPost("create")]
    public async Task<ActionResult<ApiResponse<ProductModel>>> CreateProduct(ProductModel product)
    {
        var productService = new ProductService(_context);
        var createdProduct = productService.Create(product);
        var response = new ApiResponse<ProductModel>(createdProduct, "Product created successfully");
        return Ok(response);
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<ApiResponse<ProductModel>>> UpdateProduct(int id, ProductModel product)
    {
        var productService = new ProductService(_context);
        var existingProduct = productService.GetById(id);
        if (existingProduct == null)
        {
            return NotFound(new ApiResponse<ProductModel>(null, "Product not found"));
        }

        var updatedProduct = productService.Update(id, product);

        var response = new ApiResponse<ProductModel>(updatedProduct, "Product updated successfully");
        return Ok(response);
    }
}