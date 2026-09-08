using UnityEngine;

public class FallState : PlayerState
{
    public FallState(Player_Base player, PlayerStateMachine sm)
        : base(player, sm) { }

    public override void Enter()
    {
        Debug.Log("Falling");
    }

    public override void Update()
    {
        base.Update();

        if (MOTOR.Grounded)
        {
            if (INPUT.movement == Vector2.left || INPUT.movement == Vector2.right)
                stateMachine.ChangeState(player.moveState);
            else
                stateMachine.ChangeState(player.idleState);
        }

        if (INPUT.jumpPressed && MOTOR.currentCoyoteTime > 0f)
        {
            stateMachine.ChangeState(player.jumpState);
        }

        /*if (INPUT.jumpPressed && MOTOR.currentCoyoteTime < 0f)
        {
            stateMachine.ChangeState(player.floatState);
        }*/
    }
}
