using Databasing.Entities;
using KindoffStore.Models.Interfaces;

namespace KindoffStore.Models;

public enum ModifyCustomerViewState
{
    Success,
    FailedNotFound,
    FailedUnspecified,
    Undefined,
    None
}

public class ModifyCustomerModel : IViewableStateMachine<ModifyCustomerViewState>
{
    private ModifyCustomerViewState screen_state = ModifyCustomerViewState.None;

    public CustomerForm user_form = new();
    public Customer old_customer_data;

    public ModifyCustomerViewState GetCurrentState()
    {
        if (this.IsStateInvalid())
            throw new NullReferenceException("ModifyCustomerModel: Model state is null.");

        return this.screen_state;
    }

    public bool IsStateInvalid()
    {
        return this.screen_state == ModifyCustomerViewState.Undefined || this.screen_state == ModifyCustomerViewState.None;
    }

    public void SetCurrentState(ModifyCustomerViewState new_state)
    {
        if (this.screen_state == new_state) return;

        if (new_state == ModifyCustomerViewState.None)
            throw new InvalidOperationException("ModifyCustomerModel.SetCurrentState(): the view state cannot be \"none\", it may be undefined behaviour.");

        this.screen_state = new_state;
    }
}
