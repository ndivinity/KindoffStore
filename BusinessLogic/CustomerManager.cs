using BusinessLogic.Core;
using Databasing;
using Databasing.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace BusinessLogic;

public class CustomerManager: ICSingletonify
{
    private readonly IDbContextFactory<DatabaseContext> ctx_factory;

    public const string PlaceholderPfpUrl = "https://i.pinimg.com/1200x/c3/76/08/c37608774ef4c67d56c74365c1cf455f.jpg";

    public CustomerManager(IDbContextFactory<DatabaseContext> ctx_factory)
    {
        this.ctx_factory = ctx_factory;
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        DatabaseContext db = this.ctx_factory.CreateDbContext();

        return db.customers.AsEnumerable();
    }

    public static bool DeepCompareCustomer(Customer customer_a, Customer customer_b)
    {
        if(customer_a.customer_name != customer_b.customer_name)
        {
            return false;
        } else if(customer_a.customer_address != customer_b.customer_address)
        {
            return false;
        } else if(customer_a.customer_telephone != customer_b.customer_telephone)
        {
            return false;
        } else if(customer_a.customer_photo_url != customer_b.customer_photo_url)
        {
            return false;
        }

            return true;
    }

    public Customer? GetCustomerById(int customer_id)
    {
        DatabaseContext db = this.ctx_factory.CreateDbContext();

        return db.customers.Where(c => c.customer_id == customer_id).FirstOrDefault();
    }

    public Customer? GetCustomerByMatch(Customer customer_details)
    {
        DatabaseContext db = this.ctx_factory.CreateDbContext();

        foreach(Customer c in db.customers)
        {
            if (CustomerManager.DeepCompareCustomer(c, customer_details)) return c;
        }

        return null;
    }

    public bool CreateCustomer(Customer customer_details)
    { 
        // Check if the customer already exists.
        if(this.GetCustomerByMatch(customer_details) != null)
        {
            return false;
        }

        DatabaseContext db = this.ctx_factory.CreateDbContext();

        db.customers.Add(customer_details);
        return db.SaveChanges() > 0;
    }

    public bool RemoveCustomerById(int customer_id)
    {
        DatabaseContext db = this.ctx_factory.CreateDbContext();

        Customer find = this.GetCustomerById(customer_id)!;

        if(find == null)
        {
            return false;
        } else
        {
            db.customers.Remove(find);
            return db.SaveChanges() > 0;
        }
    }

    public bool RemoveCustomerByMatch(Customer customer_details)
    {
        DatabaseContext db = this.ctx_factory.CreateDbContext();

        // Check if exists
        Customer find = this.GetCustomerByMatch(customer_details)!;

        if(find == null)
        {
            return false;
        } else
        {
            db.customers.Remove(find);
            return db.SaveChanges() > 0;
        }
    }

    public bool ModifyCustomerById(int customer_id, Customer c_details)
    {
        DatabaseContext db = this.ctx_factory.CreateDbContext();

        // Check if exists
        Customer customer = this.GetCustomerById(customer_id)!;
        if (customer == null) return false;

        // TODO: Again, this is not proper modifying, is delete/add, and it's ass.
        db.customers.Remove(customer);
        c_details.customer_photo_url = CustomerManager.PlaceholderPfpUrl;
        db.customers.Add(c_details);
        db.SaveChanges();
        return true;
    }
}
