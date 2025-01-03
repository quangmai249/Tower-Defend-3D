using TMPro;
using UnityEngine;

public class ButtonShop : MonoBehaviour
{
    [SerializeField] GameObject turret;
    [SerializeField] GameObject confirm;
    [SerializeField] TextMeshPro textPrice;

    private GameManager gameManager;
    private UIManager uiManager;
    private AudioManager audioManager;
    private SingletonTurrets singletonTurrets;
    private TurretStats turretStats;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        singletonTurrets = SingletonTurrets.Instance;
        audioManager = AudioManager.Instance;

        uiManager = SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagUIManager).GetComponent<UIManager>();
    }
    private void Start()
    {
        this.gameObject.SetActive(true);
        this.confirm.SetActive(false);
        this.turretStats = this.turret.gameObject.GetComponent<Turrets>().GetTurretStats();
        this.textPrice.text = $"-{turretStats.PriceTurret.ToString()}$";
    }
    public void ButtonSelectTurret()
    {
        if (this.turret.tag == GameObjectTagManager.TagFirstTurret)
            this.turret.gameObject.GetComponent<Turrets>().DisplayTurretStats();
        if (this.turret.tag == GameObjectTagManager.TagSecondTurret)
            this.turret.gameObject.GetComponent<Turrets>().DisplayTurretStats();
        if (this.turret.tag == GameObjectTagManager.TagThirdTurret)
            this.turret.gameObject.GetComponent<Turrets>().DisplayTurretStats();
        if (this.turret.tag == GameObjectTagManager.TagFourthTurret)
            this.turret.gameObject.GetComponent<Turrets>().DisplayTurretStats();

        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagButtonConfirmShopTurret);
        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagButtonConfirmShopTurret);
        this.confirm.SetActive(true);
        return;
    }
    public void ButtonConfirmSelectTurret()
    {
        if (gameManager.GameStats.Gold < this.turretStats.PriceTurret)
        {
            uiManager.SetActiveTextNotEnoughGold(true);
            uiManager.SetTextNotEnoughGold($"You do not have enough money to build {(this.turret.name).Replace("(Clone)", "")}!");
            return;
        }

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
