using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] GameObject panelGameOver;
    [SerializeField] GameObject panelGameWin;
    [SerializeField] GameObject panelSetting;
    [SerializeField] GameObject panelPauseGame;
    [SerializeField] GameObject panelConfrimSurrender;

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

    [Header("Volume")]
    [SerializeField] TextMeshProUGUI textVolumeMusic;
    [SerializeField] TextMeshProUGUI textVolumeFXSound;

    [Header("Stats")]
    [SerializeField] string textTurretStatsTag = "Text Turret Stats";
    [SerializeField] string imgTurretStatsTag = "Image Turret Stats";

    private GameManager gameManager;
    private GameStats gameStats;
    private AudioManager audioManager;
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
        audioManager = AudioManager.Instance;
        gameManager = GameManager.Instance;

        gameStats = gameManager.GameStats;

        this.toggleMuteMusic.isOn = true;
        this.toggleMuteFXSound.isOn = true;
        this.toggleFullScreen.isOn = true;

        this.textNotEnoughGold.text = string.Empty;
        this.panelGameOver.SetActive(false);
        this.panelGameWin.SetActive(false);
        this.panelSetting.SetActive(false);
        this.panelPauseGame.SetActive(false);
        this.panelConfrimSurrender.SetActive(false);
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

        textVolumeMusic.text = $"{Mathf.Round(audioManager.VolumeMusic * 100).ToString()}";
        textVolumeFXSound.text = $"{Mathf.Round(audioManager.VolumeFXSound * 100).ToString()}";
    }
    public bool ToggleMusic { get => this.toggleMuteMusic.isOn; }
    public bool ToggleFXSound { get => this.toggleMuteFXSound.isOn; }
    public void SetToggleMusic(bool isOn)
    {
        toggleMuteMusic.isOn = isOn;
    }
    public void SetToggleFXSound(bool isOn)
    {
        toggleMuteFXSound.isOn = isOn;
    }
    public bool GetToogleFullScreen()
    {
        return toggleFullScreen.isOn;
    }
    public void ButtonConfirmSurrender()
    {
        this.panelPauseGame.gameObject.SetActive(false);
        this.panelConfrimSurrender.gameObject.SetActive(true);
    }
    public void ButtonNoConfirmSurrender()
    {
        this.panelConfrimSurrender.gameObject.SetActive(false);
        this.panelPauseGame.gameObject.SetActive(true);
    }
    public void ButtonSetting()
    {
        this.panelPauseGame.gameObject.SetActive(false);
        this.panelPauseGame.gameObject.SetActive(true);
    }
    public void ButtonExitSetting()
    {
        if (toggleFullScreen.isOn == true)
            Screen.fullScreen = true;
        else
            Screen.fullScreen = false;

        this.panelPauseGame.SetActive(true);
        this.panelSetting.SetActive(false);
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