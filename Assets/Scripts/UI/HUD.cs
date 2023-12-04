using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private Transform healthBarPlayer1;
    private Transform healthBarPlayer2;

    private const string PLAYER1_HEALTH_BAR_PATH = "Player1/HealthBar";
    private const string PLAYER2_HEALTH_BAR_PATH = "Player2/HealthBar";

    private void Awake()
    {
        healthBarPlayer1 = transform.Find(PLAYER1_HEALTH_BAR_PATH);
        healthBarPlayer2 = transform.Find(PLAYER2_HEALTH_BAR_PATH);
    }

    private void Start()
    {
        PlayerOne.OnHealthChanged += OnPlayer1HealthChanged;
        PlayerTwo.OnHealthChanged += OnPlayer2HealthChanged;
    }

    private void OnDestroy()
    {
        PlayerOne.OnHealthChanged -= OnPlayer1HealthChanged;
        PlayerTwo.OnHealthChanged -= OnPlayer2HealthChanged;
    }

    private void OnPlayer1HealthChanged(float health)
    {
        healthBarPlayer1.GetComponent<Slider>().value = health / 100;
    }

    private void OnPlayer2HealthChanged(float health)
    {
        healthBarPlayer2.GetComponent<Slider>().value = health / 100;
    }
}
