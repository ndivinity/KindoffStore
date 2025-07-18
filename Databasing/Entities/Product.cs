using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databasing.Entities
{
    public enum ProductWeightUnit
    {
        Litres,
        Mililitres,
        Kilograms,
        Miligrams,
        Grams
    }

    public class Product
    {
        // Mutting off the nullable warning because this fields should NEVER be null.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable IDE1006 // Naming Styles
        [Key]
        [Required]
        public int product_id { get; set; }
        [Required] public int product_existence { get; set; }
        [Required] public string product_name { get; set; }
        [Required] public string product_description { get; set; }
        [Required] public int product_weight { get; set; }
        [Required] public ProductWeightUnit product_weight_unit { get; set; }
        [Required] public decimal product_price { get; set; }

        // public virtual ICollection<Product> products { get; set; }
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
