using Databasing;
using Microsoft.AspNetCore.Mvc;

namespace KindoffStore.Controllers;

using MvcRouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

[MvcRoute("/Client")]public class ClientController : Controller
{
    private readonly ILogger<ClientController> logger;
    private readonly DatabaseContext dc;

    #region Clients Controller Initialiser
    public ClientController(ILogger<ClientController> logger, DatabaseContext dc)
    {
        this.logger = logger;
        this.dc = dc;
    }
    #endregion

    #region Endpoints
    public async Task<IActionResult> Index()
    {
        return await Task.FromResult(View());
    }
    #endregion
}
