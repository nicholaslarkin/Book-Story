using Unity.VisualScripting;
using UnityEngine;

public class Player_Motor : MonoBehaviour
{
    #region VARIABLES
    [Header("Am I Real or a Dummy?")]
    public bool amReal;

    [Header("Instance")]
    public static Player_Motor Instance;
    public Player_Base playerBase;
    private PageManager pageManager;

    [Header("Components")]
    public Vector3 velocity;
    Vector2 moveInput;
    public Rigidbody rb;

    [Header("Movement Settings")]
    public float speedMultiplier;

    [Header("Movement")]
    public float moveSpeed;
    public float acceleration;
    public float airAcceleration;

    [Header("Rotation")]
    public float RotationSmoothTime;
    public float rotationVelocity;

    [Header("Gravity")]
    public bool useGravity = true;
    public float fallForce;
    public float gravityFlux;
    public float gravity;
    public float regularTerminalVelocity;

    [Header("Jump")]
    public float jumpForce;
    public float currentCoyoteTime;
    public float maxCoyoteTime;
    public int currentJumpCount;
    public int maxJumpCount;

    [Header("Float")]
    public bool Floating;
    public float floatForce;
    public float floatMoveSpeed;
    public float floatTerminalVelocity;

    [Header("Crouch")]
    public bool Crouched;
    public float crouchMoveSpeed;

    [Header("PageCheck")]
    public bool turningPage;

    [Header("Grounded")]
    public bool Grounded;
    public float GroundedOffset;
    public float GroundedRadius;
    public float upwardSlopeInfluence;
    public float downwardSlopeInfluence;
    public LayerMask GroundLayers;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerBase = GetComponent<Player_Base>();
        pageManager = FindAnyObjectByType<PageManager>();
    }

    void FixedUpdate()
    {
        velocity = rb.linearVelocity;

        if (turningPage || pageManager.atBookEnd) // if turning the page the player will stop ALL physics calculations and float in the air
        {
            rb.linearVelocity = new Vector3(0, 0, 0);
            return;
        }

        Gravity(); // handles vertical acceleration

        CoyoteTimer();

        Move(); // handles horizontal acceleration

        rb.linearVelocity = velocity;
    }

    #region MOVE
    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    public void Move()
    {
        // Take the direction the player wants to move, calculate the velocity they should be moving at, then gradually accelerate the current velocity toward that target velocity, limited by acceleration and air control

        Vector3 moveDir = new Vector3(moveInput.x, 0, 0); // direction based on input

        if (moveDir.sqrMagnitude > 1f || moveDir.sqrMagnitude < 1f) // prevents diagonals from being faster/slower than cardinals
            moveDir.Normalize();

        if (moveDir.magnitude < 0.1f) // ignore deadzone; prevents constant idle and move state changes
            moveDir = Vector3.zero;

        float movementSpeed = Crouched ? crouchMoveSpeed : Floating ? floatMoveSpeed : moveSpeed;

        Vector3 currentHorizontal = new Vector3(velocity.x, 0, velocity.z); // current velocity (Momentum)
        Vector3 desiredVelocity = moveDir * movementSpeed * speedMultiplier; // velocity we want
        float accel = Grounded ? acceleration : airAcceleration; // acceleration

        Vector3 velocityChange = desiredVelocity - currentHorizontal; // calculating diagonal movement
                                                                      // the difference between current and desired velocity

        velocityChange = Vector3.ClampMagnitude( // limiting the amount of velocity done overtime, never going beyond
            velocityChange,                      // what acceleration allows
            accel * Time.fixedDeltaTime
        );

        currentHorizontal += velocityChange; // add it back?

        velocity.x = currentHorizontal.x; // restating the movement axis with our conditions
        velocity.z = currentHorizontal.z; // restating the movement axis with our conditions

        if (playerBase.groundCheck.OnSlope())
        {
            float slopeInfluence = 0f;

            if (velocity.y > 0)
            {
                slopeInfluence = upwardSlopeInfluence;

                rb.AddForce(Vector3.down * 10f, ForceMode.Force);
            }

            if (velocity.y < 0)
            {
                slopeInfluence = downwardSlopeInfluence;

                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }

            rb.AddForce(playerBase.groundCheck.GetSlopeMoveDirection() * movementSpeed * slopeInfluence, ForceMode.Force);
        }
    }
    #endregion

    #region JUMP
    public void Jump()
    {
        float jumpingForce = jumpForce;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpingForce, rb.linearVelocity.z);
    }

    public void CoyoteTimer()
    {
        if (Grounded)
        {
            currentCoyoteTime = maxCoyoteTime;
        }
        else
        {
            currentCoyoteTime -= Time.deltaTime;
        }
    }
    #endregion

    #region GRAVITY
    public void Gravity()
    {
        //doesn't seem to work; COME BACK TO LATER; NOT A SUPER BIG DEAL
        useGravity = !playerBase.groundCheck.OnSlope(); //turn off gravity when on slopes; prevents sliding

        float terminalVelocity = Floating ? floatTerminalVelocity : regularTerminalVelocity;

        if (!Grounded && useGravity)
        {
            if (velocity.y > 0)
            {
                velocity.y += gravity * gravityFlux * Time.fixedDeltaTime; //Jumping Up

                /*if (playerBase.INPUT.jump) // controls jump height based on how long button is pressed
                    gravityFlux = 3f;
                else
                    gravityFlux = 10f;*/

                velocity.y = Mathf.Max(velocity.y, terminalVelocity);
            }
            else
            {
                float floatingForce = Floating ? floatForce : 1f;

                velocity.y -= fallForce * floatingForce * Time.fixedDeltaTime; //Falling Down
                velocity.y = Mathf.Max(velocity.y, terminalVelocity);
            }
        }
        #endregion
    }
}
