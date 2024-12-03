using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUpgradeTurrets : MonoBehaviour
{
    [SerializeField] GameObject btnConfirm;
    [SerializeField] TextMeshProUGUI textPriceUpgrade;
    [SerializeField] TextMeshProUGUI textSellTurret;

    [SerializeField] Vector3 defaultRotaion = new Vector3(90f, 0, 0);
    [SerializeField] float upgradeturretStatsPercent = 50f;

    [SerializeField] string btnConfirmUpgradeTag = "Button Confirm Upgrade Turret";
    [SerializeField] string btnConfirmShopTag = "Button Confirm Shop Turret";
    [SerializeField] string btnCanvasShopTag = "Canvas Shop Turrets";
    [SerializeField] string btnCanvasUpgradeTag = "Canvas Upgrade Turrets";
    [SerializeField] string textTurretStatsTag = "Text Turret Stats";
    //[SerializeField] string imgTurretStatsTag = "Image Turret Stats";

    private GameObject turret;
    private GameObject menuUpgrade;

    private GameManager gameManager;
    private GameStats gameStats;
    private UIManager uiManager;

    private TurretTypes turretTypes;
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
        gameStats = gameManager.GameStats;

        this.btnConfirm.SetActive(false);
        this.turret = this.gameObject.transform.parent.parent.parent.gameObject;
        this.menuUpgrade = this.gameObject.transform.parent.parent.gameObject;
    }
    private void Update()
    {
        turretStats = this.turret.GetComponent<Turrets>().GetTurretStats();

        this.textPriceUpgrade.text = $"-{this.turretStats.PriceUpgradeTurret.ToString()}$ (+{this.upgradeturretStatsPercent.ToString()}% all)";
        this.textSellTurret.text = $"+{this.turretStats.PriceSellTurret.ToString()}$";

        this.menuUpgrade.transform.rotation = Quaternion.Euler(this.defaultRotaion);
    }
    public void ButtonUpgradeTurret()
    {
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmUpgradeTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnCanvasShopTag);
        this.btnConfirm.SetActive(true);
        return;
    }
    public void ButtonConfirmUpgradeTurret()
    {
        if (gameStats.Gold < this.turretStats.PriceUpgradeTurret)
        {
            uiManager.SetActiveTextNotEnoughGold(true);
            uiManager.SetTextNotEnoughGold($"You do not have enough money to upgrade {(this.turret.name).Replace("(Clone)", "")}!");
            return;
        }
        else if (this.turretStats.LevelTurret == 5)
        {
            uiManager.SetActiveTextNotEnoughGold(true);
            uiManager.SetTextNotEnoughGold($"{(this.turret.name).Replace("(Clone)", "")} was max level!");
            return;
        }

        uiManager.SetActiveTextNotEnoughGold(false);
        gameStats.Gold -= this.turretStats.PriceUpgradeTurret;

        this.StartUpgradeTurretStats();
        this.SetTextStats(true);
        this.btnConfirm.SetActive(false);
        return;
    }
    public void ButtonSellTurret()
    {
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmShopTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmUpgradeTag);
        this.btnConfirm.SetActive(true);
        return;
    }
    public void ButtonConfirmSellTurret()
    {
        gameStats.Gold += this.turretStats.PriceSellTurret;
        this.SetTextStats(false);

        singletonBuilding.InstantiateAt(this.turret.transform.position);
        this.turret.gameObject.SetActive(false);

        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmShopTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmUpgradeTag);

        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnCanvasShopTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnCanvasUpgradeTag);
        return;
    }
    private void StartUpgradeTurretStats()
    {
        TurretStats res = this.turretStats;

        res.LevelTurret++;
        res.PriceTurret = this.turretStats.PriceTurret;
        res.PriceUpgradeTurret += Mathf.Round(this.turretStats.PriceUpgradeTurret * (upgradeturretStatsPercent / 100));
        res.PriceSellTurret += Mathf.Round(this.turretStats.PriceSellTurret * (upgradeturretStatsPercent / 100));
        res.RangeTurret += this.turretStats.RangeTurret * (upgradeturretStatsPercent / 300);
        res.DamagedTurret += this.turretStats.DamagedTurret * (upgradeturretStatsPercent / 100);

        //if (this.turret.GetComponent<BulletLaser>() != null)
        //    res.RateTurret += this.turretStats.RateTurret * (upgradeturretStatsPercent / 300);

        //if (this.turret.GetComponent<BulletSimple>() != null)
        //    res.RateTurret -= this.turretStats.RateTurret * (upgradeturretStatsPercent / 300);

        this.turret.GetComponent<Turrets>().SetTurretStats(res);
    }
    private void SetTextStats(bool isActive)
    {
        GameObject goText = SelectTarget.SelectFirstGameObjectWithTag(this.textTurretStatsTag);
        if (isActive == false)
            goText.GetComponent<TextMeshProUGUI>().text = string.Empty;
        else
        {
            goText.GetComponent<TextMeshProUGUI>().text = $"Level: {this.turretStats.LevelTurret}\n";
            goText.GetComponent<TextMeshProUGUI>().text += $"Name: {this.turret.gameObject.name.Replace("(Clone)", "")}\n";
            goText.GetComponent<TextMeshProUGUI>().text += $"Damage: {this.turretStats.DamagedTurret}\n";
            goText.GetComponent<TextMeshProUGUI>().text += $"Range: {this.turretStats.RangeTurret}\n";
            goText.GetComponent<TextMeshProUGUI>().text += $"Rate: {this.turretStats.RateTurret}\n";
        }
    }
}
