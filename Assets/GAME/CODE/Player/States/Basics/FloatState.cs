using UnityEngine;

public class FloatState : PlayerState
{
    public FloatState(Player_Base player, PlayerStateMachine sm) : base(player, sm) { }

    public override void Enter()
    {
        MOTOR.Floating = true;

        Debug.Log("Floating");
    }

    public override void Update()
    {
        base.Update();

        /*if (player.rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }*/

        if (INPUT.jumpReleased || MOTOR.Grounded)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        MOTOR.Floating = false;  
    }
}
