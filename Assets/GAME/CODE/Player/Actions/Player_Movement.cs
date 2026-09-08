using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Player_Movement : Player_Components
{
    // IDLE & WALKING AUDIO PLAYS IN HERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    void FixedUpdate()
    {
        SendInputToMotor();
    }

    void SendInputToMotor()
    {
        /*if (!MOTOR.canMove)
        {
            MOTOR.SetMoveInput(Vector2.zero);
            return;
        }*/

        Vector2 input = INPUT.movement;

        if (input == Vector2.zero)
        {
            player.motor.SetMoveInput(Vector2.zero);
            return;
        }

        //Vector2 move = input.y + input.x;

        // Send as XZ
        MOTOR.SetMoveInput(new Vector2(input.x, input.y));
    }
}
