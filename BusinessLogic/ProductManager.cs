
using Databasing;
using Databasing.Entities;

using Microsoft.EntityFrameworkCore;

namespace BusinessLogic;

// This is a singleton fr
// At least EF says it will be.
public class ProductManager
{
    private readonly IDbContextFactory<DatabaseContext> ctx_factory;

    public ProductManager(IDbContextFactory<DatabaseContext> ctx_factory)
    {
        this.ctx_factory = ctx_factory;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        DatabaseContext db = this.ctx_factory.CreateDbContext();

        return db.products.AsEnumerable();
    }

    public static bool DeepCompareProduct(Product product_a, Product product_b)
    {
        if (product_a.product_id != product_b.product_id)
        {
            return false;
        } else if(product_a.product_name != product_b.product_name)
        {
            return false;
        } else if(product_a.product_description != product_b.product_description)
        {
            return false;
        } else if(product_a.product_existence != product_b.product_existence)
        {
            return false;
        } else if(product_a.product_weight != product_b.product_weight)
        {
            return false;
        } else if (product_a.product_weight_unit != product_b.product_weight_unit)
        {
            return false;
        } else if(product_b.product_price != product_a.product_price)
        {
            return false;
        }

        return true;
    }

    public Product? GetProductById(int product_id)
    {
        DatabaseContext db = this.ctx_factory.CreateDbContext();

        return db.products.Where(p => p.product_id == product_id).FirstOrDefault();
    }

    public Product? GetProductByMatch(Product product)
    {
        DatabaseContext db = this.ctx_factory.CreateDbContext();

        foreach(Product p in db.products) {
            if (ProductManager.DeepCompareProduct(p, product)) return p;
        }

        return null;
    }

    public bool ModifyProductById(int product_id, Product new_product_data)
    {
        DatabaseContext db = this.ctx_factory.CreateDbContext();

        // First check if the product exists
        // and leave firstly if doesn't
        // exist.
        if(this.GetProductById(product_id) == null)
        {
            return false;
        }

        // Modify product
        // TODO: Do further checks
        db.products.Update(new_product_data);
        return true;
    }

    public bool CreateProduct(Product details)
    {
        DatabaseContext db = this.ctx_factory.CreateDbContext();

        db.products.Add(details);
        db.SaveChanges();

        return true;
    }

    public bool RemoveProductByMatch(Product prod)
    {
        DatabaseContext db = this.ctx_factory.CreateDbContext();

        db.products.Remove(prod);
        db.SaveChanges();

        return true;
    }
}