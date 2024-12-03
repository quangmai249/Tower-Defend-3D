using TMPro;
using UnityEngine;

public class ButtonShop : MonoBehaviour
{
    [SerializeField] GameObject turret;
    [SerializeField] GameObject confirm;
    [SerializeField] TextMeshPro textPrice;
    [SerializeField] string btnConfirmShopTurretTag = "Button Confirm Shop Turret";
    [SerializeField] string btnConfirmUpgradeTurretTag = "Button Confirm Upgrade Turret";

    private SingletonTurrets singletonTurrets;
    private SingletonBuilding singletonBuilding;
    private TurretStats turretStats;

    private GameObject menuShop;
    private void Awake()
    {
        singletonTurrets = SingletonTurrets.Instance;
        singletonBuilding = SingletonBuilding.Instance;
        turretStats = this.turret.gameObject.GetComponent<Turrets>().GetTurretStats();
    }
    private void Start()
    {
        this.gameObject.SetActive(true);
        this.confirm.SetActive(false);
        this.textPrice.text = $"-{turretStats.PriceTurret.ToString()}$";
        this.menuShop = this.gameObject.transform.parent.parent.gameObject;
    }
    public void ButtonSelectTurret()
    {
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmShopTurretTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmUpgradeTurretTag);
        this.confirm.SetActive(true);
        return;
    }
    public void ButtonConfirmSelectTurret()
    {
        StartBuildingTurret();
        return;
    }
    private void StartBuildingTurret()
    {
        if (this.turret.tag == "Blue Turret")
            singletonTurrets.InstantiateBlueTurretsAt(this.gameObject.transform.parent.transform.position);
        if (this.turret.tag == "Second Turret")
            singletonTurrets.InstantiateSecondTurretsAt(this.gameObject.transform.parent.transform.position);
        if (this.turret.tag == "Third Turret")
            singletonTurrets.InstantiateThirdTurretsAt(this.gameObject.transform.parent.transform.position);
        if (this.turret.tag == "Yellow Turret")
            singletonTurrets.InstantiateYellowTurretsAt(this.gameObject.transform.parent.transform.position);

        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmShopTurretTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmUpgradeTurretTag);
        this.gameObject.transform.parent.parent.parent.gameObject.SetActive(false);
    }
}
