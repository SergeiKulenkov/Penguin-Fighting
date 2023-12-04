using UnityEngine;
using System;

public class PauseU : MonoBehaviour
{
    public static event Action OnResumePressed;
    public static event Action OnRestartPressed;

    public void OnResumeButtonPressed()
    {
        OnResumePressed?.Invoke();
    }

    public void OnRestartButtonPressed()
    {
        OnRestartPressed?.Invoke();
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
}
