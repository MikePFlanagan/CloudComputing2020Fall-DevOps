using System.Collections.Generic;
using System.Linq;
using Database;
using System;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _dbContext;

    public ProductRepository(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Product> GetAllProducts()
    {
        try
        {
            return _dbContext.Product.ToList();
        } 
        catch(Exception ex)
        {
            Console.WriteLine("Failed to get products.", ex);
            
            return new List<Product>();
        }
    }

    public Product GetProductById(long productId)
    {
        return _dbContext.Product.SingleOrDefault(p => p.ProductId == productId);
    }

    public void AddProduct(Product product)
    {
        _dbContext.Product.Add(product);
        _dbContext.SaveChanges();
    }

    public void UpdateProduct(Product productUpdate)
    {
        var product = GetProductById(productUpdate.ProductId);

        if (product != null)
        {
            product.Name = productUpdate.Name;
            product.Price = productUpdate.Price;
            product.Count = productUpdate.Count;

            _dbContext.Update(product);

            _dbContext.SaveChanges();
        }
    }

    public void DeleteProduct(long productId)
    {
        var product = new Product { ProductId = productId };

        _dbContext.Product.Attach(product);
        _dbContext.Product.Remove(product);
        _dbContext.SaveChanges();
    }
}