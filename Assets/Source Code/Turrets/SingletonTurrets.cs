using System.Collections.Generic;
using UnityEngine;

public class SingletonTurrets : MonoBehaviour
{
    [SerializeField] int numPool = 20;

    [Header("Blue")]
    [SerializeField] string blueTurretsTag = "Blue Turret";
    [SerializeField] GameObject blueTurrets;
    [SerializeField] List<GameObject> lsBlueTurrets;

    [Header("Second Turret")]
    [SerializeField] string secondTurretTag = "Second Turret";
    [SerializeField] GameObject secondTurret;
    [SerializeField] List<GameObject> lsSecondTurrets;

    [Header("Third Turret")]
    [SerializeField] string thirdTurretTag = "Third Turret";
    [SerializeField] GameObject thirdTurret;
    [SerializeField] List<GameObject> lsThirdTurrets;

    [Header("Yellow")]
    [SerializeField] string yellowTurretsTag = "Yellow Turret";
    [SerializeField] GameObject yellowTurrets;
    [SerializeField] List<GameObject> lsYellowTurrets;

    private GameObject _turret;
    public static SingletonTurrets Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"{this.name} is NOT SINGLE!");
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        this.CreateObjectPooling(this.numPool);
    }
    public GameObject InstantiateBlueTurretsAt(Vector3 pos)
    {
        GameObject res = this.GetNodeBuildingPooling(this.blueTurrets, this.blueTurretsTag, this.lsBlueTurrets);
        res.transform.position = new Vector3(pos.x, res.transform.position.y, pos.z);
        res.GetComponent<Turrets>().SetDefaultTurret();
        res.gameObject.SetActive(true);
        return res;
    }
    public GameObject InstantiateSecondTurretsAt(Vector3 pos)
    {
        GameObject res = this.GetNodeBuildingPooling(this.secondTurret, this.secondTurretTag, this.lsSecondTurrets);
        res.transform.position = new Vector3(pos.x, res.transform.position.y, pos.z);
        res.GetComponent<Turrets>().SetDefaultTurret();
        res.gameObject.SetActive(true);
        return res;
    }
    public GameObject InstantiateThirdTurretsAt(Vector3 pos)
    {
        GameObject res = this.GetNodeBuildingPooling(this.thirdTurret, this.thirdTurretTag, this.lsThirdTurrets);
        res.transform.position = new Vector3(pos.x, res.transform.position.y, pos.z);
        res.GetComponent<Turrets>().SetDefaultTurret();
        res.gameObject.SetActive(true);
        return res;
    }
    public GameObject InstantiateYellowTurretsAt(Vector3 pos)
    {
        GameObject res = this.GetNodeBuildingPooling(this.yellowTurrets, this.yellowTurretsTag, this.lsYellowTurrets);
        res.transform.position = new Vector3(pos.x, res.transform.position.y, pos.z);
        res.GetComponent<Turrets>().SetDefaultTurret();
        res.gameObject.SetActive(true);
        return res;
    }
    private GameObject GetNodeBuildingPooling(GameObject go, string goTag, List<GameObject> lsGo)
    {
        foreach (GameObject item in lsGo)
        {
            if (item.activeSelf == false && item.tag == goTag)
                return item;
        }

        this._turret = this.InstantiateTurret(go);
        lsGo.Add(this._turret);
        return this._turret;
    }
    private void CreateObjectPooling(int defaultQuantity)
    {
        for (int i = 0; i < defaultQuantity; i++)
        {
            lsBlueTurrets.Add(InstantiateTurret(this.blueTurrets));
            lsSecondTurrets.Add(InstantiateTurret(this.secondTurret));
            lsThirdTurrets.Add(InstantiateTurret(this.thirdTurret));
            lsYellowTurrets.Add(InstantiateTurret(this.yellowTurrets));
        }
    }
    private GameObject InstantiateTurret(GameObject go)
    {
        this._turret = Instantiate(go).gameObject;
        this._turret.gameObject.transform.SetParent(this.gameObject.transform);
        this._turret.gameObject.SetActive(false);
        return this._turret;
    }
}
