using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
public class ShopTurrets : MonoBehaviour
{
    [Header("Turrets")]
    [SerializeField] GameObject turret;
    [SerializeField] TextMeshPro textPrice;

    [Header("Confirm")]
    [SerializeField] GameObject objConfirm;
    private GameObject go;

    [Header("Time Confirm Building")]
    [SerializeField] GameObject objTimeConfirm;
    private GameObject go_timeConfirm;

    [Header("Node Building Parent")]
    [SerializeField] Vector3 rangeFindNodeBuilding = new Vector3(1, 1, 1);
    [SerializeField] GameObject nodeBuildingParent;
    [SerializeField] float distanceFindNodeBuilding;
    [SerializeField] readonly string nodeBuildingTag = "Node Building";


    [Header("Stats")]
    [SerializeField] TextMeshPro textTimeBuilding;
    [SerializeField] float timeBuildingTurrets;
    [SerializeField] float defaultTimeBuildingTurrets = 2f;
    [SerializeField] bool isBuilding = false;

    private SingletonShopTurrets singletonShopTurrets;
    private SingletonTurrets singletonTurrets;
    private TurretStats turretStats;
    private void Awake()
    {
        singletonShopTurrets = SingletonShopTurrets.Instance;
        singletonTurrets = SingletonTurrets.Instance;

        this.go = Instantiate(objConfirm);
        this.go.gameObject.SetActive(false);
        this.go.transform.SetParent(this.gameObject.transform);

        this.go_timeConfirm = Instantiate(objTimeConfirm);
        this.go_timeConfirm.gameObject.SetActive(false);
        this.go_timeConfirm.transform.SetParent(this.go.transform);

        this.turretStats = this.turret.gameObject.GetComponent<Turrets>().GetTurretStats();
    }
    private void Start()
    {
        this.timeBuildingTurrets = this.defaultTimeBuildingTurrets;
        this.textTimeBuilding = this.go_timeConfirm.GetComponent<TextMeshPro>();
        this.textPrice.text = this.turretStats.PriceTurret.ToString();
    }
    private void Update()
    {
        this.textTimeBuilding.text = string.Format("{0:0.00}", timeBuildingTurrets);

        if (this.timeBuildingTurrets <= 0)
        {
            this.distanceFindNodeBuilding = Vector3.Distance(this.go.transform.parent.position, rangeFindNodeBuilding);
            this.nodeBuildingParent = SelectTarget.StartSelectTarget(this.gameObject.transform.parent.position, this.distanceFindNodeBuilding, this.nodeBuildingTag);

            if (this.nodeBuildingParent != null)
            {
                this.nodeBuildingParent.SetActive(false);
                singletonShopTurrets.SetActiveShopTurrets(false, this.nodeBuildingParent.transform.position);
            }

            singletonTurrets.SetTurretBuilding(this.turret);
            singletonTurrets
                .InstantiateTurretsAt(new Vector3(this.gameObject.transform.parent.position.x
                , this.turret.transform.position.y, this.gameObject.transform.parent.position.z));

            this.timeBuildingTurrets = defaultTimeBuildingTurrets;
            this.go.gameObject.gameObject.SetActive(false);
            this.go_timeConfirm.gameObject.SetActive(false);

            this.isBuilding = false;

            Destroy(this.nodeBuildingParent.gameObject);
            return;
        }

        if (this.isBuilding == true)
            this.timeBuildingTurrets -= Time.deltaTime;
        else
            this.timeBuildingTurrets = defaultTimeBuildingTurrets;
    }
    private void OnMouseDown()
    {
        this.go.gameObject.SetActive(true);
        this.go.transform.position
            = this.gameObject.transform.position + new Vector3(0, 0.1f, 0);

        this.go_timeConfirm.gameObject.SetActive(true);
        this.go_timeConfirm.transform.position
            = this.gameObject.transform.position + new Vector3(0, 0, 2f);

        this.isBuilding = true;
    }
    private void OnMouseUp()
    {
        this.go.gameObject.gameObject.SetActive(false);
        this.go_timeConfirm.gameObject.SetActive(false);
        this.isBuilding = false;
    }
}
