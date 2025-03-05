public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, StateManager stateManager) : base(player, stateManager)
    {
    }
    public override void EnterState()
    {
        player.animator.SetBool(constant.IDLE, true);
    }

    public override void UpdateState()
    {
        //Thực hiện logic
    }

    public override void ExitState()
    {
        player.animator.SetBool(constant.IDLE, false);
    }
}
