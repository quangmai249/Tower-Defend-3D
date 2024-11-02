using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShop : MonoBehaviour
{
    [SerializeField] GameObject turret;
    [SerializeField] GameObject confirm;
    [SerializeField] TextMeshPro textPrice;
    [SerializeField] string btnConfirmShopTurretTag = "Button Confirm Shop Turret";
    [SerializeField] string btnConfirmUpgradeTurretTag = "Button Confirm Upgrade Turret";

    private Vector3 nodePos;
    private SingletonTurrets singletonTurrets;
    private TurretStats turretStats;

    private GameObject nodeBuildingParent;
    private GameObject menuShop;
    private void Awake()
    {
        singletonTurrets = SingletonTurrets.Instance;
        turretStats = this.turret.gameObject.GetComponent<Turrets>().GetTurretStats();
    }
    private void Start()
    {
        this.gameObject.SetActive(true);
        this.confirm.SetActive(false);

        this.textPrice.text = $"-{turretStats.PriceTurret.ToString()}$";

        this.nodeBuildingParent = this.gameObject.transform.parent.parent.parent.gameObject;
        this.menuShop = this.gameObject.transform.parent.parent.gameObject;

        if (this.nodeBuildingParent == null)
        {
            Debug.LogError("Node Building Parent is NULL!");
            return;
        }

        nodePos = this.nodeBuildingParent.transform.position;
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
        if (this.turret.tag == "White Turret")
            singletonTurrets.InstantiateBlueTurretsAt(this.nodePos);
        else if (this.turret.tag == "Blue Turret")
            singletonTurrets.InstantiateBlueTurretsAt(this.nodePos);
        else if (this.turret.tag == "Red Turret")
            singletonTurrets.InstantiateRedTurretsAt(this.nodePos);
        else if (this.turret.tag == "Yellow Turret")
            singletonTurrets.InstantiateYellowTurretsAt(this.nodePos);

        this.nodeBuildingParent.gameObject.SetActive(false);
    }
}
