using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
public class ShopTurrets : MonoBehaviour
{
    [SerializeField] GameObject turret;

    [Header("Confirm")]
    [SerializeField] GameObject objConfirm;
    [SerializeField] GameObject go;

    [Header("Time Confirm Building")]
    [SerializeField] GameObject objTimeConfirm;
    [SerializeField] GameObject go_timeConfirm;

    [Header("Node Building Parent")]
    [SerializeField] GameObject nodeBuildingParent;
    [SerializeField] readonly string nodeBuildingTag = "Node Building";

    [SerializeField] TextMeshPro textTimeBuilding;

    [SerializeField] float timeBuildingTurrets;
    [SerializeField] float defaultTimeBuildingTurrets = 2f;
    [SerializeField] bool isBuilding = false;

    private SingletonShopTurrets singletonShopTurrets;
    private SingletonTurrets singletonTurrets;
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
    }
    private void Start()
    {
        this.timeBuildingTurrets = this.defaultTimeBuildingTurrets;
        this.textTimeBuilding = this.go_timeConfirm.GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        this.textTimeBuilding.text = string.Format("{0:0.00}", timeBuildingTurrets);

        if (this.timeBuildingTurrets <= 0)
        {
            singletonTurrets.SetTurretBuilding(this.turret);
            singletonTurrets.InstantiateTurretsAt(this.gameObject.transform.parent.position);

            this.nodeBuildingParent = FindGameObjectWithPos(this.gameObject.transform.parent.position, nodeBuildingTag);
            this.nodeBuildingParent.SetActive(false);
            singletonShopTurrets.SetActiveShopTurrets(false, this.nodeBuildingParent.transform.position);

            this.timeBuildingTurrets = defaultTimeBuildingTurrets;
            this.go.gameObject.gameObject.SetActive(false);
            this.go_timeConfirm.gameObject.SetActive(false);

            this.isBuilding = false;
            Destroy(this.nodeBuildingParent);
            return;
        }

        if (this.isBuilding == true)
            this.timeBuildingTurrets -= Time.deltaTime;
        else
            this.timeBuildingTurrets = defaultTimeBuildingTurrets;
    }
    private void OnMouseDown()
    {
        if (this.gameObject.name.Equals("Cancel"))
        {
            singletonShopTurrets.SetActiveShopTurrets(false, this.transform.position);
            return;
        }

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
    private GameObject FindGameObjectWithPos(Vector3 pos, string tag)
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag(tag))
            if (g.transform.position == pos)
                return g;
        return null;
    }
}
