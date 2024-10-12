using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject panelGameOver;
    [SerializeField] TextMeshProUGUI textNumberRoundSurvived;
    [SerializeField] TextMeshProUGUI textNotEnoughGold;
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
        this.textNotEnoughGold.text = string.Empty;
        this.panelGameOver.SetActive(false);
    }
    private void Update()
    {
        textGold.text = $"{gameStats.Gold}$";
        textLives.text = $"{gameStats.Lives} LIVES";

        if (gameManager.GetIsGameOver() == true)
        {
            this.panelGameOver.SetActive(true);
            this.textNumberRoundSurvived.text = (gameStats.WaveStart).ToString();

            DOTween.KillAll();
            Time.timeScale = 0f;
            return;
        }
    }
    public void SetTextNotEnoughGold(string text)
    {
        this.textNotEnoughGold.text = text;
    }
    public void SetActiveTextNotEnoughGold(bool b)
    {
        this.textNotEnoughGold.gameObject.SetActive(b);
    }
}
