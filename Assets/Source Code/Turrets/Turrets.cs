using System.Collections;
using TMPro;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    [SerializeField] GameObject nodeBuilding;

    [Header("Turret Stats")]
    [SerializeField] float priceTurrets = 100f;
    [SerializeField] float priceUpgrade = 50f;
    [SerializeField] float priceSell = 50f;
    [SerializeField] float range = 6f;
    [SerializeField] float damage = 10f;

    private Renderer rend;
    private Color color;

    private SingletonBuilding singletonBuilding;
    private SingletonUpgradeTurrets singletonUpgradeTurrets;

    private GameManager gameManager;
    private UIManager uiManager;

    private GameStats gameStats;
    private TurretStats turretStats;
    private void Awake()
    {
        singletonBuilding = SingletonBuilding.Instance;
        singletonUpgradeTurrets = SingletonUpgradeTurrets.Instance;
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;

        gameStats = gameManager.GetGameStats();
    }
    private void Start()
    {
        turretStats = new TurretStats(this.priceTurrets, this.priceUpgrade, this.priceSell, this.range, this.damage);

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
    }
    private void OnMouseEnter()
    {
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
        singletonUpgradeTurrets.SetActiveUpgradeTurrets(true, new Vector3(this.gameObject.transform.position.x, 0, this.transform.position.z));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (turretStats != null && gameObject != null)
            Gizmos.DrawWireSphere(gameObject.transform.position, turretStats.RangeTurret);
    }
    public TurretStats GetTurretStats()
    {
        return new TurretStats(this.priceTurrets, this.priceUpgrade, this.priceSell, this.range, this.damage);
    }
}
