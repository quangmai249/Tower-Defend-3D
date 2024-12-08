using TMPro;
using UnityEngine;

public class ButtonShop : MonoBehaviour
{
    [SerializeField] GameObject turret;
    [SerializeField] GameObject confirm;
    [SerializeField] TextMeshPro textPrice;
    [SerializeField] string btnConfirmShopTurretTag = "Button Confirm Shop Turret";
    [SerializeField] string btnConfirmUpgradeTurretTag = "Button Confirm Upgrade Turret";
    [SerializeField] string canvasUpgradeTurretTag = "Canvas Upgrade Turrets";

    private AudioManager audioManager;
    private SingletonTurrets singletonTurrets;
    private TurretStats turretStats;
    private void Awake()
    {
        singletonTurrets = SingletonTurrets.Instance;
        audioManager = AudioManager.Instance;
    }
    private void Start()
    {
        this.gameObject.SetActive(true);
        this.confirm.SetActive(false);
        turretStats = this.turret.gameObject.GetComponent<Turrets>().GetTurretStats();
        this.textPrice.text = $"-{turretStats.PriceTurret.ToString()}$";
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
        if (this.turret.tag == "First Turret")
            singletonTurrets.InstantiateFirstTurretsAt(this.gameObject.transform.parent.transform.position);
        if (this.turret.tag == "Second Turret")
            singletonTurrets.InstantiateSecondTurretsAt(this.gameObject.transform.parent.transform.position);
        if (this.turret.tag == "Third Turret")
            singletonTurrets.InstantiateThirdTurretsAt(this.gameObject.transform.parent.transform.position);
        if (this.turret.tag == "Fourth Turret")
            singletonTurrets.InstantiateFourthTurretsAt(this.gameObject.transform.parent.transform.position);

        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmShopTurretTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmUpgradeTurretTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.canvasUpgradeTurretTag);

        this.gameObject.transform.parent.parent.parent.gameObject.SetActive(false);

        audioManager.ActiveAudioBuilding(true);
    }
}
