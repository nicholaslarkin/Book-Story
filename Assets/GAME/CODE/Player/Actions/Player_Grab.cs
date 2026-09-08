using UnityEngine;

public class Player_Grab : Player_Components
{
    /*bool wasGrounded;

    void Update()
    {
        if (MOTOR.Grounded || MOTOR.currentGrabCooldown <= 0)
        {
            MOTOR.canGrab = true;
            MOTOR.currentGrabCooldown = MOTOR.maxGrabCooldown;
        }

        if (INPUT.attack)
        {
            StartGrab();
        }

        StopGrab();
    }

    void StartGrab()
    {
        if (!MOTOR.canGrab)
            return;

        MOTOR.canGrab = false;
        player.motor.RequestGrab();
    }

    public void StopGrab()
    {
        if (!MOTOR.grabRequested && !MOTOR.canGrab)
        {
            MOTOR.currentGrabCooldown -= Time.deltaTime;
        }
    }*/
}