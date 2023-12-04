using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    public static event Action OnPausePressed;
    public static event Action OnResumePressed;

    private const string PLAYER1_MOVEMENT = "Player1Movement";
    private const string PLAYER2_MOVEMENT = "Player2Movement";

    private void Awake()
    {
        IsPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPaused) OnPausePressed?.Invoke();
            else OnResumePressed?.Invoke();
        }
    }

    public static void SetIsPaused(bool isPaused) => IsPaused = isPaused;

    public static float GetMovementInput(bool isPlayer1)
    {
        float movement = 0;
        if (!IsPaused)
        {
            if (isPlayer1) movement = Input.GetAxis(PLAYER1_MOVEMENT);
            else movement = Input.GetAxis(PLAYER2_MOVEMENT);
        }

        return movement;
    }

    public static bool IsAttackPressed(bool isPlayer1)
    {
        bool isAttackPressed = false;
        if (!IsPaused)
        {
            if (isPlayer1) isAttackPressed = Input.GetKeyDown(KeyCode.Space);
            else isAttackPressed = Input.GetMouseButtonDown(0);
        }

        return isAttackPressed;
    }

    public static bool IsJumpPressed(bool isPlayer1)
    {
        bool isJumpPressed = false;
        if (!IsPaused)
        {
            if (isPlayer1) isJumpPressed = Input.GetKeyDown(KeyCode.W);
            else isJumpPressed = Input.GetKeyDown(KeyCode.UpArrow);
        }

        return isJumpPressed;
    }

    public static bool IsSlidePressed(bool isPlayer1)
    {
        bool isSlidePressed = false;
        if (!IsPaused)
        {
            if (isPlayer1) isSlidePressed = Input.GetKey(KeyCode.LeftShift);
            else isSlidePressed = Input.GetKey(KeyCode.RightShift);
        }

        return isSlidePressed;
    }
}
