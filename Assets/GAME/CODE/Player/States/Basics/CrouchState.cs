using UnityEngine;

public class CrouchState : PlayerState
{
    public CrouchState(Player_Base player, PlayerStateMachine sm) : base(player, sm) { }

    public override void Enter()
    {
        MOTOR.Crouched = true;

        Debug.Log("Crouch");
    }

    public override void Update()
    {
        base.Update();

        /*if (player.rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }*/

        if (INPUT.jumpPressed)
        {
            return;
        }

        if (INPUT.movement.y != -1)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        MOTOR.Crouched = false;
    }
}