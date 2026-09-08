using UnityEngine;

public class Player_Jump : Player_Components
{
    /*bool wasGrounded;

    void Update()
    {
        if (!wasGrounded && MOTOR.Grounded)
        {
            MOTOR.currentJumpCount = MOTOR.maxJumpCount;
        }

        if (MOTOR.currentJumpCount == 0)
        {
            MOTOR.canJump = false;
        }

        if (INPUT.jump && MOTOR.currentJumpCount > 0)
        {
            StartJump();
        }

        wasGrounded = MOTOR.Grounded;
    }

    void StartJump()
    {
        if (!MOTOR.canJump || !MOTOR.canMove)
            return;

        MOTOR.currentJumpCount--;

        player.motor.RequestJump();
    }

    void StopJump()
    {
        
    }*/
}