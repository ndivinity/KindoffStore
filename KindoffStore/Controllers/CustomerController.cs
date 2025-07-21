using BusinessLogic;
using Databasing;
using Databasing.Entities;
using KindoffStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace KindoffStore.Controllers;

using MvcRouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

[MvcRoute("/Customer")]public class CustomerController : Controller
{
    private readonly ILogger<CustomerController> logger;
    private readonly CustomerManager cm;

    #region Customer Controller Initialiser
    public CustomerController(ILogger<CustomerController> logger, CustomerManager cm)
    {
        this.logger = logger;
        this.cm = cm;
    }
    #endregion

    #region Endpoints
    public IActionResult Index()
    {
        IEnumerable<Customer> c_list = this.cm.GetAllCustomers();
        return View(new CustomerModel(c_list));
    }

    [HttpGet("/Customer/CreateCustomer")]
    public IActionResult CreateCustomer()
    {
        return View(new CustomerCreateModel());
    }

    [AcceptVerbs("Post")]
    [HttpPost("/Customer/CreateCustomerResponse")]
    public IActionResult CreateCustomerResponse(string userform_name, string userform_address, string userform_telephone)
    {
        CustomerCreateResponseModel model = new();

        Customer uri_constructed = new()
        {
            customer_name = userform_name,
            customer_address = userform_address,
            customer_telephone = userform_telephone,
            customer_photo_url = CustomerManager.PlaceholderPfpUrl
        };


        // Check if exists firsthand
        Customer find = this.cm.GetCustomerByMatch(uri_constructed)!;

        if (find != null)
        {
            model.SetCurrentState(CustomerCreateViewState.CreateFailedExists);
            model.old_customer = find!;
            return View(model);
        }

        if(this.cm.CreateCustomer(uri_constructed))
        {
            model.SetCurrentState(CustomerCreateViewState.CreateSuccess);
            model.new_customer = uri_constructed;

            return View(model);
        } else
        {
            // TODO: Maybe put a more explicit error state here.
            // That'd involve getting more info from the
            // customer manager.
            model.SetCurrentState(CustomerCreateViewState.CreateFailedUndefined);
            return View(model);
        }
    }

    [HttpGet("/Customer/ModifyCustomer")]
    public IActionResult ModifyCustomer(int customer_id)
    {
        // Check if exists and get it's reference
        Customer customer = this.cm.GetCustomerById(customer_id)!;
        ModifyCustomerModel model = new();
        model.old_customer_data = customer!;

        if(customer == null)
        {
            model.SetCurrentState(ModifyCustomerViewState.FailedNotFound);
            return View(model);
        }

        model.old_customer_data = customer;
        return View(model);
    }

    [AcceptVerbs("Post")]
    [HttpPost("/Customer/ModifyCustomerResponse")]
    public IActionResult ModifyCustomerResponse(int userform_id, string userform_name, string userform_address, string userform_telephone)
    {
        Customer find = this.cm.GetCustomerById(userform_id)!;
        ModifyCustomerResponseModel model = new();

        if (find == null)
        {
            model.SetCurrentState(CustomerModifyResponseViewState.FailedNotExist);
            return View(model);
        }
        else
        {
            Customer uri_constructed = new()
            {
                customer_name = userform_name,
                customer_address = userform_address,
                customer_telephone = userform_telephone
            };

            if(this.cm.ModifyCustomerById(userform_id, uri_constructed))
            {
                model.SetCurrentState(CustomerModifyResponseViewState.Success);
                model.old_customer_data = find;
                model.new_customer_data = uri_constructed;
                return View(model);
            } else
            {
                model.SetCurrentState(CustomerModifyResponseViewState.FailedUndefined);
                return View(model);
            }
        }
    }

    [AcceptVerbs("Post")]
    [HttpPost("/Customer/DeleteCustomer")]
    public IActionResult DeleteCustomer(int customer_id)
    {
        Customer c = this.cm.GetCustomerById(customer_id)
        ?? throw new NullReferenceException($"Failed to fetch customer with id {customer_id}");

        this.cm.RemoveCustomerByMatch(c);

        return RedirectToAction("Index");
    }
    #endregion
}
