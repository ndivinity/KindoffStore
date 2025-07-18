using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace KindoffStore.Controllers
{
    using MvcRouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

    [MvcRoute("")] public class SalesController : Controller
    {
        private readonly ILogger<SalesController> logger;
        private readonly object? sales = null;

        public SalesController(ILogger<SalesController> logger, object? sales = null)
        {
            this.logger = logger;
            this.sales = sales;
        }

        [HttpGet("/Sales/Index")] public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View());
        }
    }
}
