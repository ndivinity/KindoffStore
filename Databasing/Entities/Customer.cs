
using System.ComponentModel.DataAnnotations;

namespace Databasing.Entities;

public class Customer
{
#pragma warning disable IDE1006 // Naming Styles
    [Required][Key] public int customer_id { get; set; }
    [Required] public string customer_name { get; set; }
    [Required] public string customer_address { get; set; }
    [Required] public string customer_telephone { get; set; }
    [Required] public string customer_photo_url { get; set; }
#pragma warning restore IDE1006 // Naming Styles
}
