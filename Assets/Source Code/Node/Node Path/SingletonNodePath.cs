using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonNodePath : MonoBehaviour
{
    [SerializeField] GameObject nodePath;
    public static SingletonNodePath Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"{this.name} is NOT SINGLE!");
            return;
        }
        Instance = this;
    }
    public GameObject InstantiateNodePathAt(Vector3 pos)
    {
        return Instantiate(this.nodePath, pos, this.nodePath.transform.rotation);
    }
}