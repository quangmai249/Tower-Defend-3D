using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject panelGameOver;
    [SerializeField] GameObject panelGameWin;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI textLevelWin;
    [SerializeField] TextMeshProUGUI textNumberRoundSurvived;
    [SerializeField] TextMeshProUGUI textNotEnoughGold;
    [SerializeField] TextMeshProUGUI textGold;
    [SerializeField] TextMeshProUGUI textLives;
    [SerializeField] TextMeshProUGUI textFPS;

    [Header("Stats")]
    [SerializeField] string textTurretStatsTag = "Text Turret Stats";
    [SerializeField] string imgTurretStatsTag = "Image Turret Stats";

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
        gameStats = gameManager.GameStats;

        this.textNotEnoughGold.text = string.Empty;
        this.panelGameOver.SetActive(false);
        this.panelGameWin.SetActive(false);
    }
    private void Update()
    {
        textGold.text = $"{gameStats.Gold}$";
        textLives.text = $"{gameStats.Lives} LIVES";

        if (gameManager.IsGameWinLevel == true)
        {
            this.panelGameWin.SetActive(true);
            this.textLevelWin.text = PlayerPrefs.GetString("LEVEL").ToString();
            this.HiddenObjectsWhenOver();
            return;
        }

        if (gameManager.IsGameOver == true)
        {
            this.panelGameOver.SetActive(true);
            this.textNumberRoundSurvived.text = (gameStats.WaveStart).ToString();
            this.HiddenObjectsWhenOver();
            return;
        }

        textFPS.text = $"{Mathf.Round(1f / Time.unscaledDeltaTime).ToString()} FPS";
    }
    private void HiddenObjectsWhenOver()
    {
        SelectTarget.SelectFirstGameObjectWithTag(this.textTurretStatsTag).GetComponent<TextMeshProUGUI>().text = string.Empty;
        SelectTarget.SelectFirstGameObjectWithTag(this.imgTurretStatsTag).GetComponent<RawImage>().texture = null;
        SelectTarget.SelectFirstGameObjectWithTag(this.imgTurretStatsTag).GetComponent<RawImage>().color = Color.clear;

        DOTween.KillAll();
        Time.timeScale = 0f;
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
