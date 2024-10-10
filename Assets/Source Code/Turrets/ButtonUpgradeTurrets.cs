using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonUpgradeTurrets : MonoBehaviour
{
    [SerializeField] GameObject btnConfirm;
    [SerializeField] TextMeshProUGUI textPriceUpgrade;
    [SerializeField] TextMeshProUGUI textSellTurret;
    [SerializeField] string btnConfirmTag = "Button Confirm Upgrade Turret";

    private GameObject turret;
    private GameObject menuUpgrade;

    private GameManager gameManager;
    private GameStats gameStats;
    private UIManager uiManager;

    private TurretStats turretStats;
    private SingletonBuilding singletonBuilding;
    private void Awake()
    {
        singletonBuilding = SingletonBuilding.Instance;
        uiManager = UIManager.Instance;
        gameManager = GameManager.Instance;
    }
    private void Start()
    {
        gameStats = gameManager.GetGameStats();

        this.btnConfirm.SetActive(false);
        this.turret = this.gameObject.transform.parent.parent.parent.gameObject;
        this.menuUpgrade = this.gameObject.transform.parent.parent.gameObject;
    }
    private void Update()
    {
        turretStats = this.turret.GetComponent<Turrets>().GetTurretStats();

        this.textPriceUpgrade.text = this.turretStats.PriceUpgradeTurret.ToString();
        this.textSellTurret.text = this.turretStats.PriceSellTurret.ToString();

        this.menuUpgrade.transform.rotation = Quaternion.Euler(75f, 0, 0);
    }
    public void ButtonCloseUpgradeTurret()
    {
        this.menuUpgrade.SetActive(false);
        return;
    }
    public void ButtonUpgradeTurret()
    {
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmTag);
        this.btnConfirm.SetActive(true);
        return;
    }
    public void ButtonConfirmUpgradeTurret()
    {
        if (gameStats.Gold < this.turretStats.PriceUpgradeTurret)
        {
            uiManager.SetActiveTextNotEnoughGold(true);
            uiManager.SetTextNotEnoughGold($"You do not have enough money to upgrade" +
                $" {(this.turret.name).Replace("(Clone)", "")}!");
            return;
        }
        uiManager.SetActiveTextNotEnoughGold(false);
        gameStats.Gold -= this.turretStats.PriceUpgradeTurret;

        //
        TurretStats res = this.turretStats;
        res.PriceTurret = this.turretStats.PriceTurret;
        res.PriceUpgradeTurret += Mathf.Round(this.turretStats.PriceUpgradeTurret * 0.5f);
        res.PriceSellTurret += Mathf.Round(this.turretStats.PriceSellTurret * 0.75f);
        res.RangeTurret += this.turretStats.RangeTurret * 0.1f;
        res.DamagedTurret += this.turretStats.DamagedTurret * 0.25f;
        //

        this.turret.GetComponent<Turrets>().SetTurretStats(res);

        this.btnConfirm.SetActive(false);
        return;
    }
    public void ButtonSellTurret()
    {
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmTag);
        this.btnConfirm.SetActive(true);
        return;
    }
    public void ButtonConfirmSellTurret()
    {
        singletonBuilding.InstantiateAt(this.turret.transform.position);
        gameStats.Gold += this.turretStats.PriceSellTurret;

        Destroy(this.turret.gameObject);
        return;
    }
}
