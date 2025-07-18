using KindoffStore.Models;

using System.Diagnostics;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

using SQLitePCL;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.X509Certificates;

using Databasing;
using Databasing.Entities;
using BusinessLogic;

namespace KindoffStore.Controllers
{
    using MvcRouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

    [MvcRoute("/Product")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> logger;
        private readonly ProductManager pm;

<<<<<<< Updated upstream
        public ProductController(ILogger<ProductController> logger, ProductsContext pdc)
        {
            this.logger = logger;
            this.pdc = pdc;
        }

=======
        #region Product Controller Initialiser
        public ProductController(ILogger<ProductController> logger, ProductManager pm)
        {
            this.logger = logger;
            this.pm = pm;
        }
        #endregion


        #region Endpoints
>>>>>>> Stashed changes
        [HttpGet("/Product/Index")]
        public IActionResult Index()
        {
            return View(this.pm.GetAllProducts() as IEnumerable<Product>);
        }

        [HttpGet("/Product/CreateProduct")]
        public IActionResult CreateProduct()
        {
            int iota = 0;

            List<SelectListItem> items = [
                new SelectListItem { Text = "Kilograms", Value = $"{iota++}" },
                new SelectListItem { Text = "Grams", Value = $"{iota++}", Selected = true },
                new SelectListItem { Text = "Litres", Value = $"{iota++}" },
                new SelectListItem { Text = "Mililitres", Value = $"{iota++}" },
            ];

            ViewBag.userform_weight_unit = items;

            return View();
        }

<<<<<<< Updated upstream
        public async Task<IActionResult> GetProduct(object? unused)
=======
        [HttpGet("/Product/ModifyProduct")]
        public IActionResult ModifyProduct(int product_id)
        {
            int iota = 0;

            List<SelectListItem> items = [
                new SelectListItem { Text = "Kilograms", Value = $"{iota++}" },
                new SelectListItem { Text = "Grams", Value = $"{iota++}", Selected = true },
                new SelectListItem { Text = "Litres", Value = $"{iota++}" },
                new SelectListItem { Text = "Mililitres", Value = $"{iota++}" },
            ];

            ViewBag.userform_weight_unit = items;
            //ViewBag.original_product_data = this.DoesProductExistById(product_id);
            ViewData["original_product_data"] = this.pm.GetProductById(product_id);

            return View();
        }

        [HttpPost("/Product/ModifyProductResponse")]
        public IActionResult ModifyProductResponse(int userform_id, string userform_name, string userform_description, int userform_weight, int userform_weight_unit, int userform_existence)
        {
            Console.WriteLine($"Trying to modify product {userform_id}");

            Product uri_constructed = new()
            {
                product_id = userform_id,
                product_name = userform_name,
                product_description = userform_description,
                product_weight = userform_weight,
                product_weight_unit = (ProductWeightUnit)userform_weight_unit,
            };

            Product? find = this.pm.GetProductById(userform_id);

            if (find == null)
            {
                ViewData["screen_state"] = "ProductNotExistOnModify";
                return View();
            } else
            {
                uri_constructed.product_id = find.product_id;
                this.pm.ModifyProductById(userform_id, uri_constructed);
            }

            return CreatedAtAction(
                    nameof(this.GetProduct),
                    new { }, uri_constructed
            );
        }

        public IActionResult GetProduct(object? unused)
>>>>>>> Stashed changes
        {
            return null! as IActionResult;
        }

        [HttpPost("/Product/CreateProductResponse")]
        public IActionResult CreateProductResponse(string userform_name, string userform_description, int userform_weight, int userform_weight_unit, int userform_existence)
        {
            Product constructed = new()
            {
                product_name = userform_name,
                product_description = userform_description,
                product_weight = userform_weight,
                product_weight_unit = (ProductWeightUnit)userform_weight_unit,
                product_existence = userform_existence
            };

<<<<<<< Updated upstream
            // Verify if the product that's being tried to create
            // already exists.
            bool exists =
                this.pdc.products.Where(product => product.product_name == userform_name).
                FirstOrDefault()
                is not null;
            
            if (!exists)
            {
                // If doesn't exist, just create it.
                await this.pdc.AddAsync(constructed);

                await this.pdc.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(this.GetProduct),
                    new { }, constructed
                );
=======

            if (this.pm.GetProductByMatch(constructed) == null)
            {
                // If doesn't exist, just create it.
                this.pm.CreateProduct(constructed);

                return RedirectToAction("Index");
>>>>>>> Stashed changes
            }
            else
            {
                // Otherwise tweak and error out.
                ViewData["screen_state"] = "ProductExists";
                return View();
            }
        }

        [HttpDelete("/Product/DeletePost")]
        [HttpPost("/Product/DeletePost")]
        public IActionResult DeleteProduct(int product_id)
        {
<<<<<<< Updated upstream
            Product prod = this.pdc.products
            .Where(product => product.product_id == product_id)
            .FirstOrDefault()!
            ?? throw new NullReferenceException($"Failed to fetch post with id {product_id}");

            this.pdc.products.Remove(prod);
            await this.pdc.SaveChangesAsync();

            // return RedirectToAction("Index");

            ViewData["deleted_object"] = prod;
=======
            Product prod = this.pm.GetProductById(product_id)
            ?? throw new NullReferenceException($"Failed to fetch post with id {product_id}");

            this.pm.RemoveProductByMatch(prod);
>>>>>>> Stashed changes

            return await Task.FromResult(View());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}