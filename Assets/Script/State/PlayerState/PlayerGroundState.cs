public class PlayerGroundState : IState
{
    protected readonly Player player;
    protected readonly StateManager stateManager;

    public PlayerGroundState(Player player, StateManager stateManager)
    {
        this.player = player;
        this.stateManager = stateManager;
    }

    public virtual void EnterState()
    {

    }

    public virtual void UpdateState()
    {

    }

    public virtual void ExitState()
    {

    }

    public virtual void FixUpdateState()
    {
        
    }

    public virtual void switchState(IState state)
    {
        
    }
}
