using Databasing.Entities;

using KindoffStore.Models.Interfaces;

namespace KindoffStore.Models;

public class CustomerModel
{
    public IEnumerable<Customer> customer_list { get; set; } = null;

    public CustomerModel(IEnumerable<Customer> c_list)
    {
        this.customer_list = c_list ?? throw new ArgumentNullException(nameof(c_list), "Customer list cannot be null");
    }
}
