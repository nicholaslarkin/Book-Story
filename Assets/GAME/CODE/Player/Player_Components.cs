using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Components : MonoBehaviour
{
    /// Initializes component of PlayerInput and PlayerMotor
    /// All Player scripts inherit from this and use these to make clean, readable references to their components

    /// EX - instead of player.input.canDash, now it's INPUT.canDash, for everything that inherits this script

    protected Player_Base player;
    protected Inputs_ButtonBindings INPUT;
    protected Player_Motor MOTOR;

    //protected Animator animator;

    protected virtual void Start()
    {
        player = GetComponent<Player_Base>();

        //animator = GetComponent<Animator>();

        INPUT = player.INPUT;
        MOTOR = player.MOTOR;

        //anytime there needs to be something initialized in Start with PlayerComponent inheritance, put it here
        MOTOR.currentJumpCount = MOTOR.maxJumpCount; // <--- STUPID THING HERE
                                                     // need to put here to make sure it initializes correctly
    }
}
