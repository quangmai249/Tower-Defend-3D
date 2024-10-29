using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    [SerializeField] GameObject nodeBuilding;
    [SerializeField] GameObject menuUpgradeTurrets;
    [SerializeField] string canvasShopTag = "Canvas Shop Turrets";
    [SerializeField] string canvasUpgradeTag = "Canvas Upgrade Turrets";
    [SerializeField] string btnConfirmTag = "Button Confirm Upgrade Turret";
    [SerializeField] float yPos = 2f;

    [Header("Stats")]
    [SerializeField] string textTurretStatsTag = "Text Turret Stats";

    [Header("Turret Stats")]
    [SerializeField] TurretTypes turretTypes = TurretTypes.Simple;
    [SerializeField] float priceTurrets = 100f;
    [SerializeField] float priceUpgrade = 50f;
    [SerializeField] float priceSell = 50f;
    [SerializeField] float range = 6f;
    [SerializeField] float damage = 10f;

    private Renderer rend;
    private Color color;
    private GameObject upgradeTurrets;

    private SingletonBuilding singletonBuilding;
    private GameManager gameManager;
    private UIManager uiManager;

    private GameStats gameStats;
    private TurretStats turretStats;
    private void Awake()
    {
        singletonBuilding = SingletonBuilding.Instance;
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;

        gameStats = gameManager.GameStats;
    }
    private void Start()
    {
        turretStats = new TurretStats(this.priceTurrets, this.priceUpgrade, this.priceSell, this.range, this.damage);

        this.upgradeTurrets
            = Instantiate(menuUpgradeTurrets, new Vector3(this.gameObject.transform.position.x, this.yPos, this.gameObject.transform.position.z)
                , menuUpgradeTurrets.transform.rotation);
        this.upgradeTurrets.transform.SetParent(this.gameObject.transform);
        this.upgradeTurrets.gameObject.SetActive(false);

        if (gameStats.Gold < turretStats.PriceTurret)
        {
            uiManager.SetActiveTextNotEnoughGold(true);
            uiManager.SetTextNotEnoughGold($"You do not have enough money to build {(this.gameObject.name).Replace("(Clone)", "")}!");

            GameObject nodeBuilding = singletonBuilding.InstantiateAt(this.gameObject.transform.position);
            nodeBuilding.transform.parent = this.gameObject.transform.parent.transform;
            Destroy(this.gameObject);
        }
        else
        {
            uiManager.SetActiveTextNotEnoughGold(false);
            gameStats.Gold -= turretStats.PriceTurret;
            this.rend = GetComponent<Renderer>();
            this.color = this.rend.material.color;
        }
    }
    private void Update()
    {
        this.priceTurrets = this.turretStats.PriceTurret;
        this.priceUpgrade = this.turretStats.PriceUpgradeTurret;
        this.priceSell = this.turretStats.PriceSellTurret;
        this.range = this.turretStats.RangeTurret;
        this.damage = this.turretStats.DamagedTurret;
    }
    private void OnMouseEnter()
    {
        if (gameManager.IsGameOver == true || gameManager.IsGamePause == true)
            return;

        rend.material.color = Color.green;
    }
    private void OnMouseExit()
    {
        rend.material.color = this.color;
    }
    private void OnMouseDown()
    {
        if (gameManager.IsGameOver == true || gameManager.IsGamePause == true)
            return;

        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.canvasShopTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.canvasUpgradeTag);

        this.upgradeTurrets.gameObject.SetActive(true);
        this.SetTextStats();
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
        GameObject go = SelectTarget.SelectFirstGameObjectWithTag(this.textTurretStatsTag);
        go.GetComponent<TextMeshProUGUI>().text = $"Name: {this.gameObject.name.Replace("(Clone)", "")}\n";
        go.GetComponent<TextMeshProUGUI>().text += $"Damage: {this.damage}\n";
        go.GetComponent<TextMeshProUGUI>().text += $"Range: {this.range}\n";
        SetRateFireTextStatsOf(go);
    }
    private void SetRateFireTextStatsOf(GameObject go)
    {
        if (this.turretTypes.ToString().Equals("Simple"))
        {
            if (this.gameObject.GetComponent<BulletSimple>() != null)
                go.GetComponent<TextMeshProUGUI>().text += $"Rate of fire: {this.gameObject.GetComponent<BulletSimple>().GetFireCountdown()}\n";
            else
                Debug.Log($"Please check Component Bullet in this Turret!");
        }

        if (this.turretTypes.ToString().Equals("Laser"))
        {
            if (this.gameObject.GetComponent<BulletLaser>() != null)
                go.GetComponent<TextMeshProUGUI>().text += $"Slow down Rate of fire: {this.gameObject.GetComponent<BulletLaser>().GetTimeSlowing()}\n";
            else
                Debug.Log($"Please check Component Bullet in this Turret!");
        }
    }
    public TurretStats GetTurretStats()
    {
        return new TurretStats(Mathf.Round(this.priceTurrets)
            , Mathf.Round(this.priceUpgrade)
            , Mathf.Round(this.priceSell)
            , this.range
            , this.damage);
    }
    public void SetTurretStats(TurretStats tStats)
    {
        this.turretStats = tStats;
    }
    public TurretTypes GetTurretTypes()
    {
        return this.turretTypes;
    }
}
