using UnityEngine;

public class PageState : PlayerState
{
    private PageDirection direction;

    public PageState(Player_Base player, PlayerStateMachine sm) : base(player, sm) { }

    public void SetDirection(PageDirection newDirection)
    {
        direction = newDirection;
    }

    public override void Enter()
    {
        if (!pageManager.VerifyPageManagerFunctionality(direction))
        {
            Debug.Log("PageManager Check Failed!");
            MOTOR.turningPage = false;
            stateMachine.ChangeState(player.fallState);
            return;
        }

        if (MOTOR.amReal)
        {
            pageManager.TurnThePage(direction);
            pageManager.CreatePlayerPageProjection();
        }

        MOTOR.turningPage = true;

        Debug.Log("Turning the Page!");
    }

    public override void Update()
    {
        if (pageManager.PageAnimationFinished)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        MOTOR.turningPage = false;
    }
}