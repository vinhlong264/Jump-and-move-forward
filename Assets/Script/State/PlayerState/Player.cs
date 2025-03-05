using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator { get; private set; }

    #region State
    public StateManager stateManager { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerAirState airState { get; private set; }
    #endregion

    private void Awake()
    {
        stateManager = new StateManager();
        idleState = new PlayerIdleState(this, stateManager);
        airState = new PlayerAirState(this, stateManager);
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        stateManager.InitState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateManager.currentState.UpdateState();
    }
}
