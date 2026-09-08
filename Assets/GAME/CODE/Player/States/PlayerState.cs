public abstract class PlayerState
{
    protected Player_Base player;
    protected PlayerStateMachine stateMachine;
    protected Inputs_ButtonBindings INPUT;
    protected Player_Motor MOTOR;
    protected PageManager pageManager;

    public PlayerState(Player_Base player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;

        INPUT = player.INPUT;
        MOTOR = player.MOTOR;
        pageManager = player.pageManager;
    }

    public virtual void Enter() { }
    public virtual void Update()
    {
        if (!MOTOR.turningPage)
        {
            if (INPUT.previousPagePressed)
            {
                player.pageState.SetDirection(PageDirection.Previous);
                stateMachine.ChangeState(player.pageState);
            }
            else if (INPUT.nextPagePressed)
            {
                player.pageState.SetDirection(PageDirection.Next);
                stateMachine.ChangeState(player.pageState);
            }
        }
    }
    public virtual void Exit() { }
}
