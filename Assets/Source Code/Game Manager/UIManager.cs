using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] GameObject panelGameOver;
    [SerializeField] GameObject panelGameWin;
    [SerializeField] GameObject panelPauseGame;
    [SerializeField] GameObject panelConfrimSurrender;
    [SerializeField] GameObject panelInstruction;
    [SerializeField] GameObject panelReboots;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI textLevelWin;
    [SerializeField] TextMeshProUGUI textNumberRoundSurvived;
    [SerializeField] TextMeshProUGUI textNotEnoughGold;
    [SerializeField] TextMeshProUGUI textGold;
    [SerializeField] TextMeshProUGUI textLives;
    [SerializeField] TextMeshProUGUI textFPS;

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
        this.panelPauseGame.SetActive(false);
        this.panelConfrimSurrender.SetActive(false);
        this.panelInstruction.SetActive(false);
        this.panelReboots.SetActive(false);

        if (GameObject.FindGameObjectWithTag(GameObjectTagManager.TagBuildingManager).GetComponent<NodeBuildingManager>().IsError())
        {
            this.panelReboots.gameObject.SetActive(true);
            return;
        }
    }
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag(GameObjectTagManager.TagEnemySpawnManager).GetComponent<EnemySpawn>().IsError())
        {
            this.panelReboots.gameObject.SetActive(true);
            return;
        }

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
    public void ButtonDisplayInstruction()
    {
        this.panelInstruction.SetActive(!this.panelInstruction.activeSelf);
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
    private void HiddenObjectsWhenOver()
    {
        SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagTextTurretStats).GetComponent<TextMeshProUGUI>().text = string.Empty;
        SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagImageTurretStats).GetComponent<RawImage>().texture = null;
        SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagImageTurretStats).GetComponent<RawImage>().color = Color.clear;

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
