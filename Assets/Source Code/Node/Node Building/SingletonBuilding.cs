using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SingletonBuilding : MonoBehaviour
{
    [SerializeField] GameObject nodeBuilding;
    public static SingletonBuilding Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"{this.name} is NOT SINGLE!");
            return;
        }
        Instance = this;
    }
    public GameObject InstantiateAt(Vector3 pos)
    {
        GameObject res = Instantiate(this.nodeBuilding);
        res.transform.SetParent(this.gameObject.transform, false);
        res.transform.localPosition = new Vector3(pos.x, this.nodeBuilding.transform.position.y, pos.z);
        return res;
    }
}
