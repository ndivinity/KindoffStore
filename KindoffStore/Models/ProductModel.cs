using Databasing.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindoffStore.Models;

public class ProductModel
{
    public List<int> products_ids = [];
    public List<int> products_existences = [];
    public List<string> products_names = [];
    public List<string> products_descriptions = [];
    public List<int> products_weights = [];
    public List<ProductWeightUnit> products_weight_units = [];

    public ProductModel() {}
}