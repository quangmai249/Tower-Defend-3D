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
    [SerializeField] string btnConfirmTag = "Button Confirm Shop Turret";

    private Vector3 nodePos;
    private SingletonTurrets singletonTurrets;
    private TurretStats turretStats;

    private GameObject nodeBuildingParent;
    private GameObject menuShop;
    private void Awake()
    {
        singletonTurrets = SingletonTurrets.Instance;
        turretStats = this.turret.GetComponent<Turrets>().GetTurretStats();
    }
    private void Start()
    {
        this.gameObject.SetActive(true);
        this.confirm.SetActive(false);
        this.textPrice.text = turretStats.PriceTurret.ToString();

        this.nodeBuildingParent = this.gameObject.transform.parent.parent.parent.gameObject;
        this.menuShop = this.gameObject.transform.parent.parent.gameObject;

        if (this.nodeBuildingParent == null)
        {
            Debug.LogError("Node Building Parent is NULL!");
            return;
        }

        nodePos = this.nodeBuildingParent.transform.position;
    }
    public void ButtonCloseShopTurret()
    {
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmTag);

        this.menuShop.gameObject.SetActive(false);
        return;
    }
    public void ButtonSelectTurret()
    {
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmTag);

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
        singletonTurrets.SetTurretBuilding(this.turret);
        singletonTurrets.InstantiateTurretsAt(this.nodePos);
        Destroy(this.nodeBuildingParent.gameObject);
    }
}
