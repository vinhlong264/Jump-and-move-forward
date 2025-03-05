using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : IState
{
    protected readonly Player player;
    protected readonly StateManager stateManager;

    public PlayerAirState(Player player, StateManager stateManager)
    {
        this.player = player;
        this.stateManager = stateManager;
    }

    public void EnterState()
    {
        
    }

    public void UpdateState()
    {
        
    }

    public void ExitState()
    {
        
    }

    public void FixUpdateState()
    {
        throw new System.NotImplementedException();
    }

    public void switchState(IState state)
    {
        throw new System.NotImplementedException();
    }
}
