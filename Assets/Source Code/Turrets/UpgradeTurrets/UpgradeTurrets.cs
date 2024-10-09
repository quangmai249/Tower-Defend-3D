using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeTurrets : MonoBehaviour
{
    [SerializeField] Vector3 vecRangeFindTurrets = new Vector3(1f, 1f, 1f);
    [SerializeField] TextMeshPro textPrice;
    [SerializeField] GameObject turret;

    [SerializeField] float distance;
    [SerializeField] string turretsTag = "Turrets";

    private SingletonUpgradeTurrets singletonUpgradeTurrets;
    private SingletonBuilding singletonBuilding;
    private GameManager gameManager;
    private void Start()
    {
        singletonUpgradeTurrets = SingletonUpgradeTurrets.Instance;
        singletonBuilding = SingletonBuilding.Instance;
        gameManager = GameManager.Instance;
    }
    private void OnMouseDown()
    {
        this.distance = Vector3.Distance(this.gameObject.transform.parent.position, this.vecRangeFindTurrets);
        this.turret = SelectTarget.StartSelectTarget(this.gameObject.transform.parent.position, this.distance, this.turretsTag);

        if (this.gameObject.name.Equals("Delete"))
        {
            singletonBuilding.InstantiateAt(this.turret.transform.position);
            Destroy(this.turret.gameObject);

            gameManager.GetGameStats().Gold += this.turret.GetComponent<Turrets>().GetTurretStats().PriceSellTurret;

            singletonUpgradeTurrets.SetActiveUpgradeTurrets(false, this.gameObject.transform.parent.position);
            return;
        }

        if (this.gameObject.name.Equals("Upgrade"))
        {
            singletonUpgradeTurrets.SetActiveUpgradeTurrets(false, this.gameObject.transform.parent.position);
            return;
        }
    }
}
