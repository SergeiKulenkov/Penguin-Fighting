using UnityEngine;
using TMPro;
using System;

public class FinishUI : MonoBehaviour
{
    public static event Action OnRestartPressed;

    private const string PLAYER_WON_TEXT_PATH = "PlayerWonText";
    private const string WON_TEXT = "WON!";

    public void SetPlayerWonText(string playerName)
    {
        TextMeshProUGUI playerWon = transform.Find(PLAYER_WON_TEXT_PATH).GetComponent<TextMeshProUGUI>();
        playerWon.text = playerName + " " + WON_TEXT;
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
