using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    [SerializeField] string textTurretStatsTag = "Text Turret Stats";

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
        this.btnConfirm.SetActive(false);
        this.SetTextStats(true);
        return;
    }
    public void ButtonSellTurret()
    {
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmUpgradeTag);
        this.btnConfirm.SetActive(true);
        return;
    }
    public void ButtonConfirmSellTurret()
    {
        singletonBuilding.InstantiateAt(this.turret.transform.position);
        gameStats.Gold += this.turretStats.PriceSellTurret;

        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmShopTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnCanvasShopTag);

        this.SetTextStats(false);
        Destroy(this.turret.gameObject);
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

        this.SetBulletRateFire();
        this.turret.GetComponent<Turrets>().SetTurretStats(res);
    }
    private void SetBulletRateFire()
    {
        BulletLaser bulletLaser = this.turret.GetComponent<BulletLaser>();
        if (bulletLaser != null)
            bulletLaser.SetTimeSlowing(bulletLaser.GetTimeSlowing() - (bulletLaser.GetTimeSlowing() * (upgradeturretStatsPercent / 300)));

        BulletSimple bulletSimple = this.turret.GetComponent<BulletSimple>();
        if (bulletSimple != null)
            bulletSimple.SetFireCountdown(bulletSimple.GetFireCountdown() - (bulletSimple.GetFireCountdown() * (upgradeturretStatsPercent / 300)));
    }
    private void SetTextStats(bool isActive)
    {
        GameObject go = SelectTarget.SelectFirstGameObjectWithTag(this.textTurretStatsTag);
        if (isActive == false)
            go.GetComponent<TextMeshProUGUI>().text = string.Empty;
        else
        {
            go.GetComponent<TextMeshProUGUI>().text = $"Level: {this.turretStats.LevelTurret}\n";
            go.GetComponent<TextMeshProUGUI>().text += $"Name: {this.turret.gameObject.name.Replace("(Clone)", "")}\n";
            go.GetComponent<TextMeshProUGUI>().text += $"Damage: {this.turretStats.DamagedTurret}\n";
            go.GetComponent<TextMeshProUGUI>().text += $"Range: {this.turretStats.RangeTurret}\n";
            SetRateFireTextStatsOf(go);
        }
    }
    private void SetRateFireTextStatsOf(GameObject go)
    {
        this.turretTypes = this.turret.GetComponent<Turrets>().GetTurretTypes();
        if (this.turretTypes.ToString().Equals("Simple"))
        {
            if (this.turret.GetComponent<BulletSimple>() != null)
            {
                go.GetComponent<TextMeshProUGUI>().text += $"Rate of fire: {this.turret.GetComponent<BulletSimple>().GetFireCountdown()}\n";
            }
            else
                Debug.Log($"Please check Component Bullet in this Turret!");
        }

        if (this.turretTypes.ToString().Equals("Laser"))
        {
            if (this.turret.GetComponent<BulletLaser>() != null)
                go.GetComponent<TextMeshProUGUI>().text += $"Slow down Rate of fire: {this.turret.GetComponent<BulletLaser>().GetTimeSlowing()}\n";
            else
                Debug.Log($"Please check Component Bullet in this Turret!");
        }
    }
}
