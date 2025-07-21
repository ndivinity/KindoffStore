namespace KindoffStore.Models;

public struct CustomerForm
{
    // the database takes care of assigning the id anyway. (auto-increment)
    public int? id { get; set; }

    public string name { get; set; }
    public string address { get; set; }
    public string telephone { get; set; }
}

public class CustomerCreateModel
{
    public CustomerForm customer_form { get; set; } = new();
}
