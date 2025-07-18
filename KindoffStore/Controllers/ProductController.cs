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


        #region Product Controller Initialiser
        public ProductController(ILogger<ProductController> logger, ProductManager pm)
        {
            this.logger = logger;
            this.pm = pm;
        }
        #endregion


        #region Endpoints
        [AcceptVerbs("Get")]
        [HttpGet("/Product/Index")]
        public IActionResult Index()
        {
            return View(this.pm.GetAllProducts() as IEnumerable<Product>);
        }

        [AcceptVerbs("Get")]
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

        public IActionResult GetProduct(object? unused)
        {
            return null! as IActionResult;
        }

        [AcceptVerbs("Get")]
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
            ViewData["original_product_data"] = this.pm.GetProductById(product_id);

            return View();
        }
        
        [HttpPost("/Product/ModifyProductResponse")]
        public IActionResult ModifyProductResponse(int userform_id, string userform_name, string userform_description, int userform_weight, int userform_weight_unit, int userform_existence, decimal userform_price)
        {
            Product find = this.pm.GetProductById(userform_id);

            if (find == null)
            {
                ViewData["screen_state"] = "ProductNotExistOnModify";
            }
            else
            {
                Product uri_constructed = new()
                {
                    product_name = userform_name,
                    product_description = userform_description,
                    product_weight = userform_weight,
                    product_weight_unit = (ProductWeightUnit)userform_weight_unit,
                    product_existence = userform_existence,
                    product_price = userform_price
                };

                ViewData["screen_state"] = this.pm.ModifyProductById(userform_id, uri_constructed)
                    ? "ProductModifiedSuccessfully"
                    : "ProductModifiedFailed"
                ;
            }

            return View();
        }

        [AcceptVerbs("Post")]
        [HttpPost("/Product/CreateProductResponse")]
        public IActionResult CreateProductResponse(string userform_name, string userform_description, int userform_weight, int userform_weight_unit, int userform_existence, decimal userform_price)
        {
            Product uri_constructed = new()
            {
                product_name = userform_name,
                product_description = userform_description,
                product_weight = userform_weight,
                product_weight_unit = (ProductWeightUnit)userform_weight_unit,
                product_existence = userform_existence,
                product_price = userform_price
            };

            // Verify if the product that's being tried to create
            // already exists.
            if (this.pm.GetProductByMatch(uri_constructed) == null)
            {
                // If doesn't exist, just create it.
                this.pm.CreateProduct(uri_constructed);

                return RedirectToAction("Index");
            }
            else
            {
                // Otherwise tweak and error out.
                ViewData["screen_state"] = "ProductExists";
                return View();
            }
        }

        [AcceptVerbs("Post")]
        [HttpPost("/Product/DeletePost")]
        public IActionResult DeleteProduct(int product_id)
        {
            Product prod = this.pm.GetProductById(product_id)
            ?? throw new NullReferenceException($"Failed to fetch post with id {product_id}");

            this.pm.RemoveProductByMatch(prod);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        #endregion
    }
}