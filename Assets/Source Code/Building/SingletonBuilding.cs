using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBuilding : MonoBehaviour
{
    [SerializeField] GameObject nodeBuilding;
    public static SingletonBuilding Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.Log($"{this.name} is not single!");
            return;
        }
        Instance = this;
    }
    public GameObject InstantiateAt(GameObject location)
    {
        return Instantiate(this.nodeBuilding, location.transform.position, this.nodeBuilding.transform.rotation);
    }
}
