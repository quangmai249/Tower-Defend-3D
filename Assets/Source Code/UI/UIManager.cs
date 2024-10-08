using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject panelGameOver;
    [SerializeField] TextMeshProUGUI textNumberRoundSurvived;

    [SerializeField] TextMeshProUGUI textGold;
    [SerializeField] TextMeshProUGUI textLives;

    private GameManager gameManager;
    private GameStats gameStats;

    public static UIManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"{this.gameObject.name} is NOT SINGLE!");
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
        gameStats = gameManager.GetGameStats();
        this.panelGameOver.SetActive(false);
    }
    private void Update()
    {
        textGold.text = $"{gameStats.GetGold()}$";
        textLives.text = $"{gameStats.GetLives()} LIVES";

        if (gameManager.GetIsGameOver() == true)
        {
            this.panelGameOver.SetActive(true);
            this.textNumberRoundSurvived.text = (gameStats.GetCurrentWave()).ToString();
            Time.timeScale = 0;
        }
    }
}
