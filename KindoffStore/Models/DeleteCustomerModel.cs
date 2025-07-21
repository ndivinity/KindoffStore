using KindoffStore.Models.Interfaces;

namespace KindoffStore.Models;

public enum CustomerDeleteViewState
{
    Success,
    FailedNotExist,
    FailedUndefined,

    Undefined,
    None
}

public class DeleteCustomerModel : IViewableStateMachine<CustomerDeleteViewState>
{
    CustomerDeleteViewState screen_state = CustomerDeleteViewState.Undefined;

    public CustomerDeleteViewState GetCurrentState()
    {
        if (this.IsStateInvalid())
            throw new NullReferenceException("CustomerDeleteModel: Model state is null.");

        return this.screen_state;
    }

    public bool IsStateInvalid()
    {
        return this.screen_state == CustomerDeleteViewState.Undefined || this.screen_state == CustomerDeleteViewState.None;
    }

    public void SetCurrentState(CustomerDeleteViewState new_state)
    {
        if (this.screen_state == new_state) return;

        if (new_state == CustomerDeleteViewState.None)
            throw new InvalidOperationException("CustomerDeleteModel.SetCurrentState(): the view state cannot be \"none\", it may be undefined behaviour.");

        this.screen_state = new_state;
    }
}
