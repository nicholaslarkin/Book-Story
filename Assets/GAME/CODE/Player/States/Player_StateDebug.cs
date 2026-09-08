using UnityEngine;

public class Player_StateDebug : Player_Components
{
    PlayerStateMachine stateMachine;

    void Awake()
    {
        stateMachine = GetComponent<PlayerStateMachine>();

        if (stateMachine == null)
            Debug.LogError("PlayerStateMachine not found!");
    }

    void OnGUI()
    {
        if (stateMachine == null || stateMachine.CurrentState == null)
            return;

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 24;
        style.normal.textColor = Color.white;

        string stateName = stateMachine.CurrentState.GetType().Name.Replace("State", "");

        GUI.Label(
            new Rect(20, 20, 400, 40),
            "State: " + stateName,
            style
        );

        GUI.Label(
            new Rect(20, 60, 400, 40),
            "Velocity: " + GetComponent<Player_Base>().rb.linearVelocity,
            style
        );

        GUI.Label(
            new Rect(20, 100, 400, 40),
            "Grounded: " + GetComponent<Player_Base>().MOTOR.Grounded,
            style
        );
    }
}
