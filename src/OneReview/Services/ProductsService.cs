

using OneReview.Domain;

namespace OneReview.Services;

public class ProductService
{
    private static readonly List<Product> ProductsRepository = [];

    //private static readonly List<User> UserRepository = [];
    public void Create(Guid userId, Product product)
    {
       // User user = UserRepository.Find(x => x.Id == userId) 
       // ??throw new InvalidOperationException($"User {userId} not found.");

       //user.AddProduct(product);

        // store the product in the database
        ProductsRepository.Add(product);
    }

    public Product? Get(Guid productId)
    {
        return ProductsRepository.Find(x => x.Id == productId);
    }
}