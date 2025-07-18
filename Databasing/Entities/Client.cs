
using System.ComponentModel.DataAnnotations;

namespace Databasing.Entities;

public class Client
{
#pragma warning disable IDE1006 // Naming Styles
    [Required][Key] public int client_id { get; set; }
    [Required] public string client_name { get; set; }
    [Required] public string client_address { get; set; }
    [Required] public string client_telephone { get; set; }
    [Required] public string client_photo_url { get; set; }
#pragma warning restore IDE1006 // Naming Styles
}
