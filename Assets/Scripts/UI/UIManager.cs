using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameObject HUD;
    private GameObject pauseUI;
    private GameObject finishUI;

    private const string CANVAS = "Canvas";
    private const string HUD_PATH = CANVAS + "/HUD";
    private const string PAUSE_UI_PATH = CANVAS + "/PauseUI";
    private const string FINISH_UI_PATH = CANVAS + "/FinishUI";
    private const string PLAYER1_TEXT = "Player 1";
    private const string PLAYER2_TEXT = "Player 2";

    private void Awake()
    {
        HUD = transform.Find(HUD_PATH).gameObject;
        pauseUI = transform.Find(PAUSE_UI_PATH).gameObject;
        finishUI = transform.Find(FINISH_UI_PATH).gameObject;
    }

    private void Start()
    {
        PlayerOne.OnDefeated += OnPlayer1Defeated; 
        PlayerTwo.OnDefeated += OnPlayer2Defeated;
        InputManager.OnPausePressed += OnPausePressed;
        InputManager.OnResumePressed += OnResumePressed;
        PauseU.OnResumePressed += OnResumePressed;
        PauseU.OnRestartPressed += OnRestartPressed;
        FinishUI.OnRestartPressed += OnRestartPressed;
    }

    private void OnDestroy()
    {
        PlayerOne.OnDefeated -= OnPlayer1Defeated; 
        PlayerTwo.OnDefeated -= OnPlayer2Defeated;
        InputManager.OnPausePressed -= OnPausePressed;
        InputManager.OnResumePressed -= OnResumePressed;
        PauseU.OnResumePressed -= OnResumePressed;
        PauseU.OnRestartPressed -= OnRestartPressed;
        FinishUI.OnRestartPressed -= OnRestartPressed;
    }

    private void OnPlayer1Defeated()
    {
        finishUI.SetActive(true);
        finishUI.GetComponent<FinishUI>().SetPlayerWonText(PLAYER2_TEXT);
    }

    private void OnPlayer2Defeated()
    {
        finishUI.SetActive(true);
        finishUI.GetComponent<FinishUI>().SetPlayerWonText(PLAYER1_TEXT);
    }

    private void OnPausePressed()
    {
        InputManager.SetIsPaused(true);
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnResumePressed()
    {
        InputManager.SetIsPaused(false);
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnRestartPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
