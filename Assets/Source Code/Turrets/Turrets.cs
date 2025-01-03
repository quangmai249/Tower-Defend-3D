using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Turrets : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] GameObject menuUpgradeTurrets;
    [SerializeField] float yPos = 2f;

    [Header("Particles")]
    [SerializeField] ParticleSystem par_upgrade;
    [SerializeField] ParticleSystem par_maxLevel;
    [SerializeField] ParticleSystem par_selected;

    [Header("Stats")]
    [SerializeField] Sprite imageTurret;

    [Header("Turret Stats")]
    [SerializeField] int levelTurret = 1;
    [SerializeField] int maxLevelTurret = 5;
    [SerializeField] float priceTurrets = 100f;
    [SerializeField] float priceUpgrade = 50f;
    [SerializeField] float priceSell = 50f;
    [SerializeField] float range = 6f;
    [SerializeField] float damage = 10f;
    [SerializeField] float rate = 1f;

    private GameObject upgradeTurrets;
    private SingletonBuilding singletonBuilding;
    private GameManager gameManager;
    private UIManager uiManager;
    private GameStats gameStats;
    private TurretStats turretStats;
    private TurretStats defaultTurretStats;

    private void Awake()
    {
        singletonBuilding = SingletonBuilding.Instance;
        gameManager = GameManager.Instance;
        gameStats = gameManager.GameStats;

        uiManager = SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagUIManager).GetComponent<UIManager>();
        this.defaultTurretStats = new TurretStats(this.priceTurrets, this.priceUpgrade, this.priceSell, this.range, this.damage, this.levelTurret, this.maxLevelTurret, this.rate);
    }
    private void Start()
    {
        this.levelTurret = 1;
        this.turretStats = this.defaultTurretStats;

        this.upgradeTurrets
            = Instantiate(menuUpgradeTurrets, new Vector3(this.gameObject.transform.position.x, this.yPos, this.gameObject.transform.position.z)
                , menuUpgradeTurrets.transform.rotation);
        this.upgradeTurrets.transform.SetParent(this.gameObject.transform);
        this.upgradeTurrets.gameObject.SetActive(false);

        if (gameStats.Gold >= turretStats.PriceTurret)
        {
            uiManager.SetActiveTextNotEnoughGold(false);
            gameStats.Gold -= turretStats.PriceTurret;
        }
    }
    private void Update()
    {
        this.levelTurret = this.turretStats.LevelTurret;
        this.priceTurrets = this.turretStats.PriceTurret;
        this.priceUpgrade = this.turretStats.PriceUpgradeTurret;
        this.priceSell = this.turretStats.PriceSellTurret;
        this.range = this.turretStats.RangeTurret;
        this.damage = this.turretStats.DamagedTurret;
        this.rate = this.turretStats.RateTurret;
    }
    private void OnMouseEnter()
    {
        if (gameManager.IsGameOver == true || gameManager.IsGamePause == true || gameManager.IsGameWinLevel == true)
            return;

        this.par_selected.gameObject.SetActive(true);
        this.par_selected.transform.localScale = Vector3.one;
        this.par_selected.transform.position = this.gameObject.transform.position + (Vector3.up / 2);

        if (this.par_selected.isPlaying == false)
        {
            this.par_selected.transform.DOScale(this.ScaleParSelected(this.gameObject.GetComponent<Turrets>().GetTurretStats().RangeTurret), 2f);
            this.par_selected.Play();
        }
    }
    private void OnMouseExit()
    {
        this.par_selected.gameObject.SetActive(false);
    }
    private void OnMouseDown()
    {
        if (gameManager.IsGameOver == true || gameManager.IsGamePause == true || gameManager.IsGameWinLevel == true)
            return;

        this.DisplayTurretStats();

        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagButtonConfirmShopTurret);
        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagButtonConfirmUpgradeTurret);

        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagCanvasUpgradeTurrets);
        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagCanvasShopTurrets);

        this.upgradeTurrets.gameObject.SetActive(true);
        return;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (turretStats != null && gameObject != null)
            Gizmos.DrawWireSphere(gameObject.transform.position, turretStats.RangeTurret);
    }
    private void SetTextStats()
    {
        GameObject go = SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagTextTurretStats);
        go.GetComponent<TextMeshProUGUI>().text = $"Level: {this.levelTurret}\n";
        go.GetComponent<TextMeshProUGUI>().text += $"Name: {this.gameObject.name.Replace("(Clone)", "")}\n";
        go.GetComponent<TextMeshProUGUI>().text += $"Damage: {this.damage}\n";
        go.GetComponent<TextMeshProUGUI>().text += $"Range: {this.range}\n";
        go.GetComponent<TextMeshProUGUI>().text += $"Rate: {this.rate}\n";

        if (this.gameObject.GetComponent<BulletLaser>() != null)
            go.GetComponent<TextMeshProUGUI>().text += $"Time Slowing: {this.gameObject.GetComponent<BulletLaser>().GetTimeSlowing()}\n";
    }
    private Vector3 ScaleParSelected(float range)
    {
        return new Vector3(range, 1, range);
    }
    private void SetImageStats()
    {
        SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagImageTurretStats).GetComponent<RawImage>().color = Color.white;
        SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagImageTurretStats).GetComponent<RawImage>().texture = this.imageTurret.texture;
    }
    public void DisplayTurretStats()
    {
        this.SetImageStats();
        this.SetTextStats();
    }
    public void SetDefaultTurret()
    {
        this.turretStats = this.defaultTurretStats;
    }
    public TurretStats GetTurretStats()
    {
        return new TurretStats(Mathf.Round(this.priceTurrets), Mathf.Round(this.priceUpgrade), Mathf.Round(this.priceSell), this.range, this.damage, this.levelTurret, this.maxLevelTurret, this.rate);
    }
    public void SetTurretStats(TurretStats tStats)
    {
        this.turretStats = tStats;
    }
    public void ActiveParUpgrade()
    {
        this.par_upgrade.Play();
    }
    public void ActiveParMaxLevelTurret()
    {
        this.par_maxLevel.Play();
    }
    public bool IsMaxLevel()
    {
        if (this.levelTurret == this.turretStats.MaxLevelTurret)
            return true;
        return false;
    }
}
