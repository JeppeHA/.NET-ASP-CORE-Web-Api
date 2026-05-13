namespace OneReview.Domain;

public class User
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public List<Product> Products { get; init; } = [];

    public void AddProduct(Product product)
    {
        if(Products.Count < 3)
        {
            Products.Add(product);
        }
    }

}