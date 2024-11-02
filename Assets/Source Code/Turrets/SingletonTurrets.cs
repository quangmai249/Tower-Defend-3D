using System.Collections.Generic;
using UnityEngine;

public class SingletonTurrets : MonoBehaviour
{
    [SerializeField] int numPool = 20;

    [Header("Blue")]
    [SerializeField] string blueTurretsTag = "Blue Turret";
    [SerializeField] GameObject blueTurrets;
    [SerializeField] List<GameObject> lsBlueTurrets;

    [Header("Red")]
    [SerializeField] string redTurretsTag = "Red Turret";
    [SerializeField] GameObject redTurrets;
    [SerializeField] List<GameObject> lsRedTurrets;

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

        this.CreateObjectPooling(this.numPool);
    }
    public GameObject InstantiateBlueTurretsAt(Vector3 pos)
    {
        GameObject res = this.GetNodeBuildingPooling(this.blueTurrets, this.blueTurretsTag, this.lsBlueTurrets);
        res.transform.position = new Vector3(pos.x, res.transform.position.y, pos.z);
        res.gameObject.SetActive(true);
        return res;
    }
    public GameObject InstantiateRedTurretsAt(Vector3 pos)
    {
        GameObject res = this.GetNodeBuildingPooling(this.redTurrets, this.redTurretsTag, this.lsRedTurrets);
        res.transform.position = new Vector3(pos.x, res.transform.position.y, pos.z);
        res.gameObject.SetActive(true);
        return res;
    }
    public GameObject InstantiateYellowTurretsAt(Vector3 pos)
    {
        GameObject res = this.GetNodeBuildingPooling(this.yellowTurrets, this.yellowTurretsTag, this.lsYellowTurrets);
        res.transform.position = new Vector3(pos.x, res.transform.position.y, pos.z);
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

        GameObject _go = Instantiate(go);
        _go.gameObject.transform.SetParent(this.gameObject.transform);
        _go.gameObject.SetActive(false);
        lsGo.Add(_go);

        return _go;
    }
    private void CreateObjectPooling(int defaultQuantity)
    {
        for (int i = 0; i < defaultQuantity; i++)
        {
            lsBlueTurrets.Add(InstantiateTurret(this.blueTurrets));
            lsRedTurrets.Add(InstantiateTurret(this.redTurrets));
            lsYellowTurrets.Add(InstantiateTurret(this.yellowTurrets));
        }
    }
    private GameObject InstantiateTurret(GameObject go)
    {
        GameObject res = Instantiate(go);
        res.gameObject.transform.SetParent(this.gameObject.transform);
        res.gameObject.SetActive(false);
        return res;
    }
}
