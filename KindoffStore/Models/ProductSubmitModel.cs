using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Databasing.Entities;

namespace KindoffStore.Models;


public class ProductRequestModel
{

}

public class ProductUserForm
{
    // FIXME: This field is marked as autoincremental in the sqlite table.
    public int id;

    [Required]
    [Display(Name = "Existence")]
    public int existence { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "A product name is required.")]
    [Display(Name = "Name")]
    [StringLength(100, MinimumLength=1, ErrorMessage="Product Name can't be empty nor bigger than 100 characters")]
    public string name { get; set; }

    [Required]
    [Display(Name = "Description")]
    [StringLength(300, MinimumLength=1, ErrorMessage="Product Description can't be empty nor bigger than 300 characters")]
    public string description { get; set; }

    [Required]
    [Display(Name="Weight")]
    public int weight { get; set; }

    [Required]
    [Display(Name = "Measure Unit")]
    public ProductWeightUnit weight_unit { get; set; }

    [Required]
    [Display(Name = "Price")]
    public decimal price { get; set; }

    public ProductUserForm() { }
}

// MINOR TODO: Fix the name consistency.
public class ProductUserRequest
{
    public ProductRequestModel requestModel { get; set; }
    public ProductUserForm userForm { get; set; }
}