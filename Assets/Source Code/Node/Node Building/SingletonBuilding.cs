using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SingletonBuilding : MonoBehaviour
{
    [SerializeField] GameObject nodeBuilding;
    [SerializeField] List<GameObject> lsNodeBuildingPooling;
    [SerializeField] int numPool = 20;
    private GameObject _nodeBuilding;
    public static SingletonBuilding Instance;
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
    public GameObject InstantiateAt(Vector3 pos)
    {
        GameObject res = this.GetNodeBuildingPooling(GameObjectTagManager.TagNodeBuilding);
        res.transform.localPosition = new Vector3(pos.x, this.nodeBuilding.transform.position.y, pos.z);
        res.SetActive(true);
        return res;
    }
    private GameObject GetNodeBuildingPooling(string tag)
    {
        foreach (GameObject item in this.lsNodeBuildingPooling)
        {
            if (item.activeSelf == false && item.tag == tag)
                return item;
        }
        this.CreateObjectPooling(this.numPool);
        return GetNodeBuildingPooling(GameObjectTagManager.TagNodeBuilding);
    }
    private void CreateObjectPooling(int defaultQuantity)
    {
        for (int i = 0; i < defaultQuantity; i++)
        {
            this._nodeBuilding = Instantiate(this.nodeBuilding);
            this._nodeBuilding.gameObject.transform.SetParent(this.gameObject.transform);
            this._nodeBuilding.gameObject.SetActive(false);
            this.lsNodeBuildingPooling.Add(this._nodeBuilding);
        }
    }
}
