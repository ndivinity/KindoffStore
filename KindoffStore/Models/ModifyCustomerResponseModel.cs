using Databasing.Entities;
using KindoffStore.Models.Interfaces;

namespace KindoffStore.Models;

public enum CustomerModifyResponseViewState
{
    Success,
    FailedNotExist,
    FailedUndefined,

    Undefined,
    None
}

public class ModifyCustomerResponseModel : IViewableStateMachine<CustomerModifyResponseViewState>
{
    CustomerModifyResponseViewState screen_state = CustomerModifyResponseViewState.None;
    public Customer old_customer_data = default!, new_customer_data = null!;

    public ModifyCustomerResponseModel() { }

    #region Viewable State Machinery interface implementation bs
    public CustomerModifyResponseViewState GetCurrentState()
    {
        if (this.IsStateInvalid())
            throw new NullReferenceException("ModifyCustomerResponseModel: Model state is null.");

        return this.screen_state;
    }

    public void SetCurrentState(CustomerModifyResponseViewState new_state)
    {
        if (this.screen_state == new_state) return;

        if (new_state == CustomerModifyResponseViewState.None)
            throw new InvalidOperationException("ModifyCustomerResponseModel.SetCurrentState(): the view state cannot be \"none\", it may be undefined behaviour.");

        this.screen_state = new_state;
    }

    public bool IsStateInvalid()
    => this.screen_state == CustomerModifyResponseViewState.Undefined || this.screen_state == CustomerModifyResponseViewState.None;
    #endregion
}
