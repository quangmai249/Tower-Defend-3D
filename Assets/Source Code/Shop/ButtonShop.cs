using TMPro;
using UnityEngine;

public class ButtonShop : MonoBehaviour
{
    [SerializeField] GameObject turret;
    [SerializeField] GameObject confirm;
    [SerializeField] TextMeshPro textPrice;

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
        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagButtonConfirmShopTurret);
        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagButtonConfirmShopTurret);
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
        if (this.turret.tag == GameObjectTagManager.TagFirstTurret)
            singletonTurrets.InstantiateFirstTurretsAt(this.gameObject.transform.parent.transform.position);
        if (this.turret.tag == GameObjectTagManager.TagSecondTurret)
            singletonTurrets.InstantiateSecondTurretsAt(this.gameObject.transform.parent.transform.position);
        if (this.turret.tag == GameObjectTagManager.TagThirdTurret)
            singletonTurrets.InstantiateThirdTurretsAt(this.gameObject.transform.parent.transform.position);
        if (this.turret.tag == GameObjectTagManager.TagFourthTurret)
            singletonTurrets.InstantiateFourthTurretsAt(this.gameObject.transform.parent.transform.position);

        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagButtonConfirmShopTurret);
        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagButtonConfirmUpgradeTurret);
        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagButtonConfirmUpgradeTurret);

        this.gameObject.transform.parent.parent.parent.gameObject.SetActive(false);

        audioManager.ActiveAudioBuilding(true);
    }
}
