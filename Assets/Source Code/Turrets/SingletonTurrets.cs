using System.Collections.Generic;
using UnityEngine;

public class SingletonTurrets : MonoBehaviour
{
    [SerializeField] int numPool = 20;

    [Header("First Turret")]
    [SerializeField] GameObject firstTurret;
    [SerializeField] List<GameObject> lsFirstTurret;

    [Header("Second Turret")]
    [SerializeField] GameObject secondTurret;
    [SerializeField] List<GameObject> lsSecondTurrets;

    [Header("Third Turret")]
    [SerializeField] GameObject thirdTurret;
    [SerializeField] List<GameObject> lsThirdTurrets;

    [Header("Fourth Turret")]
    [SerializeField] GameObject fourthTurret;
    [SerializeField] List<GameObject> lsFourthTurret;

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
    public GameObject InstantiateFirstTurretsAt(Vector3 pos)
    {
        GameObject res = this.GetNodeBuildingPooling(this.firstTurret, GameObjectTagManager.TagFirstTurret, this.lsFirstTurret);
        res.transform.position = new Vector3(pos.x, res.transform.position.y, pos.z);
        res.GetComponent<Turrets>().SetDefaultTurret();
        res.gameObject.SetActive(true);
        return res;
    }
    public GameObject InstantiateSecondTurretsAt(Vector3 pos)
    {
        GameObject res = this.GetNodeBuildingPooling(this.secondTurret, GameObjectTagManager.TagSecondTurret, this.lsSecondTurrets);
        res.transform.position = new Vector3(pos.x, res.transform.position.y, pos.z);
        res.GetComponent<Turrets>().SetDefaultTurret();
        res.gameObject.SetActive(true);
        return res;
    }
    public GameObject InstantiateThirdTurretsAt(Vector3 pos)
    {
        GameObject res = this.GetNodeBuildingPooling(this.thirdTurret, GameObjectTagManager.TagThirdTurret, this.lsThirdTurrets);
        res.transform.position = new Vector3(pos.x, res.transform.position.y, pos.z);
        res.GetComponent<Turrets>().SetDefaultTurret();
        res.gameObject.SetActive(true);
        return res;
    }
    public GameObject InstantiateFourthTurretsAt(Vector3 pos)
    {
        GameObject res = this.GetNodeBuildingPooling(this.fourthTurret, GameObjectTagManager.TagFourthTurret, this.lsFourthTurret);
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
            lsFirstTurret.Add(InstantiateTurret(this.firstTurret));
            lsSecondTurrets.Add(InstantiateTurret(this.secondTurret));
            lsThirdTurrets.Add(InstantiateTurret(this.thirdTurret));
            lsFourthTurret.Add(InstantiateTurret(this.fourthTurret));
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
