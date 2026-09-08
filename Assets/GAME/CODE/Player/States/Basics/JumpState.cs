using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(Player_Base player, PlayerStateMachine sm) : base(player, sm) { }

    public override void Enter()
    {
        MOTOR.Jump();

        MOTOR.currentCoyoteTime = 0f;

        Debug.Log("Jumping");
    }

    public override void Update()
    {
        base.Update();

        if (player.rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }

        /*if (INPUT.jumpPressed)
        {
            stateMachine.ChangeState(player.floatState);
        }*/
    }

    public override void Exit()
    {
        base.Exit();

        MOTOR.currentCoyoteTime = 0f;
    }
}
