using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(Player_Base player, PlayerStateMachine sm) : base(player, sm) { }

    public override void Enter()
    {
        Debug.Log("Idle");
    }

    public override void Update()
    {
        base.Update();

        if (!MOTOR.Grounded && player.rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if (INPUT.jumpPressed && MOTOR.currentCoyoteTime > 0f)
        {
            stateMachine.ChangeState(player.jumpState);
        }

        /*if (INPUT.movement.y == -1)
        {
            stateMachine.ChangeState(player.crouchState);
        }*/

        if (INPUT.movement == Vector2.left || INPUT.movement == Vector2.right)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}
