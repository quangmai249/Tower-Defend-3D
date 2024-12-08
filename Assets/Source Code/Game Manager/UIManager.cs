using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] GameObject panelGameOver;
    [SerializeField] GameObject panelGameWin;
    [SerializeField] GameObject pannelSetting;
    [SerializeField] GameObject pannelPauseGame;

    [Header("Toggle")]
    [SerializeField] Toggle toggleMuteMusic;
    [SerializeField] Toggle toggleMuteFXSound;
    [SerializeField] Toggle toggleFullScreen;

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

        this.toggleMuteMusic.isOn = true;
        this.toggleMuteFXSound.isOn = true;

        this.textNotEnoughGold.text = string.Empty;
        this.panelGameOver.SetActive(false);
        this.panelGameWin.SetActive(false);
        this.pannelSetting.SetActive(false);
        this.pannelPauseGame.SetActive(false);
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
    public bool GetToggleMusic()
    {
        return toggleMuteMusic.isOn;
    }
    public bool GetToggleFXSound()
    {
        return toggleMuteFXSound.isOn;
    }
    public bool GetToogleFullScreen()
    {
        return toggleFullScreen.isOn;
    }
    public void ButtonSetting()
    {
        this.pannelPauseGame.SetActive(false);
        this.pannelSetting.SetActive(true);
    }
    public void ButtonExitSetting()
    {
        if (toggleFullScreen.isOn == true)
            Screen.fullScreen = true;
        else
            Screen.fullScreen = false;

        this.pannelPauseGame.SetActive(true);
        this.pannelSetting.SetActive(false);
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
