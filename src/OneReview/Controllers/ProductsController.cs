using OneReview.Services;
using OneReview.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
namespace OneReview.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductsController(ProductService productService) : ControllerBase
{
    private readonly ProductService _productService = productService;
    
    [HttpPost]
    public IActionResult Create(CreateProductsRequest request)
    {
        User user = new User();
        // mapping to internal representation
        var product = request.ToDomain();
  
        // invoking the use case
        _productService.Create(user.Id, product);

        // mapping to external representation
        return CreatedAtAction(
            
            actionName: nameof(Get),
            routeValues: new {ProductId = product.Id},
            value: ProductResponse.FromDomain(product));
    }

    [HttpGet("{productId:guid}")]
    public IActionResult Get(Guid productId)
    {
        // invoking the use case
        var product = _productService.Get(productId);
        return product is null 
        ? Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Product not found") :
        Ok(ProductResponse.FromDomain(product));
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