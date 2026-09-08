using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class Inputs_ButtonBindings : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 movement;
    public bool jump;
    public bool jumpPressed;
    public bool jumpReleased;
    public bool previousPage;
    public bool previousPagePressed;
    public bool previousPageReleased;
    public bool nextPage;
    public bool nextPagePressed;
    public bool nextPageReleased;

#if ENABLE_INPUT_SYSTEM
    public void OnMovement(InputValue value)
    {
        MovementInput(value.Get<Vector2>());
    }

    public void OnJump(InputValue value)
    {
        bool pressed = value.isPressed;

        if (pressed && !jump)
        {
            jumpPressed = true;
        }

        if (!pressed && jump)
        {
            jumpReleased = true;
        }

        jump = pressed;
    }

    public void OnPreviousPage(InputValue value)
    {
        bool pressed = value.isPressed;

        if (pressed && !previousPage)
        {
            previousPagePressed = true;
        }

        if (!pressed && previousPage)
        {
            previousPageReleased = true;
        }

        previousPage = pressed;
    }

    public void OnNextPage(InputValue value)
    {
        bool pressed = value.isPressed;

        if (pressed && !nextPage)
        {
            nextPagePressed = true;
        }

        if (!pressed && nextPage)
        {
            nextPageReleased = true;
        }

        nextPage = pressed;
    }

#endif

    public void MovementInput(Vector2 newMoveDirection) // gives me exactly 0 and 1, and nothing in between; messes up crouching
    {
        movement = new Vector2(
        Mathf.RoundToInt(newMoveDirection.x),
        Mathf.RoundToInt(newMoveDirection.y)
    );
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void PreviousPageInput(bool newPreviousPageState)
    {
        previousPage = newPreviousPageState;
    }

    public void NextPageInput(bool newNextPageState)
    {
        nextPage = newNextPageState;
    }

    void LateUpdate()
    {
        jumpPressed = false;
        jumpReleased = false;
        previousPagePressed = false;
        previousPageReleased = false;
        nextPagePressed = false;
        nextPageReleased = false;
    }
}
