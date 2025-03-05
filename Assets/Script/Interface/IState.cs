public interface IState
{
    void EnterState();
    void UpdateState();
    void FixUpdateState();
    void ExitState();
    void switchState(IState state);
}

[System.Serializable]
public class StateManager
{
    public IState currentState;

    public void ChangeState(IState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void InitState(IState newState)
    {
        currentState = newState;
        currentState.EnterState();
    }
}
