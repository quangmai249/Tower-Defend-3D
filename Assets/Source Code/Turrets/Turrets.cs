using System.Collections;
using TMPro;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    [SerializeField] GameObject nodeBuilding;
    [SerializeField] GameObject menuUpgradeTurrets;
    [SerializeField] string canvasUpgradeTag = "Canvas Upgrade Turrets";
    [SerializeField] string btnConfirmTag = "Button Confirm Upgrade Turret";

    [Header("Turret Stats")]
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

        gameStats = gameManager.GetGameStats();
    }
    private void Start()
    {
        turretStats = new TurretStats(this.priceTurrets, this.priceUpgrade, this.priceSell, this.range, this.damage);
        this.upgradeTurrets
            = Instantiate(menuUpgradeTurrets, this.gameObject.transform.position + new Vector3(0, this.menuUpgradeTurrets.transform.position.y, 0)
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
        if (gameManager.GetIsGameOver() == true)
            return;

        rend.material.color = Color.green;
    }
    private void OnMouseExit()
    {
        rend.material.color = this.color;
    }
    private void OnMouseDown()
    {
        if (gameManager.GetIsGameOver() == true)
            return;

        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.canvasUpgradeTag);

        this.upgradeTurrets.gameObject.SetActive(true);
        return;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (turretStats != null && gameObject != null)
            Gizmos.DrawWireSphere(gameObject.transform.position, turretStats.RangeTurret);
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
}
