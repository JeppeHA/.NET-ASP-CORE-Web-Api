using OneReview.Services;
using OneReview.Domain;
using Microsoft.AspNetCore.Mvc;
using OneReview.Services;
namespace OneReview.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductsController(ProductService productService) : ControllerBase
{
    private readonly ProductService _productService = productService;
    
    [HttpPost]
    public IActionResult Create(CreateProductsRequest request)
    {
        // mapping to internal representation
        var product = request.ToDomain();
  
        // invoking the use case
        _productService.Create(product);

        // mapping to external representation
        return CreatedAtAction(
            
            actionName: nameof(Get),
            routeValues: new {ProductId = product.Id},
            value: ProductResponse.FromDomain(product));
    }

    [HttpGet("{productId:guid}")]
    public IActionResult Get(Guid productId)
    {
        // Get product
        // return 200 ok response
        return Ok(
            //Resource
        );
    }

    public record CreateProductsRequest(
        string Name,
        string Category,
        string SubCategory
    )
    {
        public Product ToDomain()
  
        {
            return new Product()
            {
                Name = Name,
                Category = Category,
                SubCategory = SubCategory,
            };
        }
    }

     public record ProductResponse(
        Guid Id,
        string Name,
        string Catgory,
        string SubCategory
    )
    {
        public static ProductResponse FromDomain(Product product)
        {
            return new ProductResponse(
                product.Id,
                product.Name,
                product.Category,
                product.SubCategory
            );
        }

    }

}