using Databasing.Entities;
using KindoffStore.Models.Interfaces;

namespace KindoffStore.Models;

public enum CustomerCreateViewState
{
    CreateSuccess,
    CreateFailedExists,
    CreateFailedUndefined,
    NotSpecified,
    Default,
    None
}

public class CustomerCreateResponseModel: IViewableStateMachine<CustomerCreateViewState>
{
    public CustomerCreateViewState screen_state = CustomerCreateViewState.None;
    public Customer new_customer = default!, old_customer = null!;

    public CustomerCreateResponseModel()
    { }

    #region View Machine State bullshittery
    public CustomerCreateViewState GetCurrentState()
    {
        if (this.IsStateInvalid())
            throw new NullReferenceException("CustomerCreateResponseModel: Model state is null.");

        return this.screen_state;
    }

    public void SetCurrentState(CustomerCreateViewState new_state)
    {
        if (this.screen_state == new_state) return;

        if (new_state == CustomerCreateViewState.None)
            throw new InvalidOperationException("CustomerCreateResponseModel.SetCurrentState(): the view state cannot be \"none\", it may be undefined behaviour.");

        this.screen_state = new_state;
    }

    public bool IsStateInvalid()
    => this.screen_state == CustomerCreateViewState.NotSpecified || this.screen_state == CustomerCreateViewState.None;
    #endregion
}