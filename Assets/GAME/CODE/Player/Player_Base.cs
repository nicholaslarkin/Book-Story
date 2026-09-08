using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Base : MonoBehaviour
{
    public static Player_Base Instance; // works with one player in scene; change for multiplayer if wanted later?

    [Header("Components")]
    public Rigidbody rb;
    //public GameObject mainCamera;
    public Player_GroundCheck groundCheck; // can possibly remove this later once this code is transferred to motor?
    public Player_Movement movement;  // can possibly remove this later once this code is transferred to motor?
    public Inputs_ButtonBindings input;

    [HideInInspector] public PlayerStateMachine stateMachine;
    [HideInInspector] public Player_Motor motor;
    [HideInInspector] public PageManager pageManager;

    public Inputs_ButtonBindings INPUT { get; private set; }
    public Player_Motor MOTOR { get; private set; }

    // STATES
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public MoveState moveState;
    [HideInInspector] public JumpState jumpState;
    [HideInInspector] public FloatState floatState;
    [HideInInspector] public CrouchState crouchState;
    [HideInInspector] public FallState fallState;
    [HideInInspector] public PageState pageState;
    [HideInInspector] public PauseState pauseState;

    void Awake()
    {
        Instance = this;

        stateMachine = GetComponent<PlayerStateMachine>();

        input = GetComponent<Inputs_ButtonBindings>();
        motor = GetComponent<Player_Motor>();
        pageManager = FindAnyObjectByType<PageManager>();

        INPUT = input;
        MOTOR = motor;

        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        movement = GetComponent<Player_Movement>(); // can possibly remove these two later once this code is transferred to motor?
        groundCheck = GetComponent<Player_GroundCheck>();

        idleState = new IdleState(this, stateMachine);
        moveState = new MoveState(this, stateMachine);
        jumpState = new JumpState(this, stateMachine);
        pageState = new PageState(this, stateMachine);
        floatState = new FloatState(this, stateMachine);
        crouchState = new CrouchState(this, stateMachine);
        fallState = new FallState(this, stateMachine);
    }

    void Start()
    {
        stateMachine.Initialize(idleState);
    }
}
