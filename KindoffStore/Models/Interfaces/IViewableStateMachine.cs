namespace KindoffStore.Models.Interfaces;

/// <summary>
/// This interface marks Models that internally can render differently
/// based on a state, in case of error/success for example.
/// </summary>
/// <typeparam name="StateMachineEnum">The enum type representing the states.</typeparam>
public interface IViewableStateMachine<StateMachineEnum>
{
    StateMachineEnum GetCurrentState();
    void SetCurrentState(StateMachineEnum new_state);
    bool IsStateInvalid();
}
