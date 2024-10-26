using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonEnemy : MonoBehaviour
{
    [SerializeField] List<GameObject> lsEnemy;
    public static SingletonEnemy Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"{this.name} is NOT SINGLE!");
            return;
        }
        Instance = this;
    }
    public GameObject InstantiateTurretsAt(Vector3 pos, GameObject parent)
    {
        int numEnemy = Random.Range(0, lsEnemy.Count);
        GameObject res = Instantiate(this.lsEnemy[numEnemy], pos, this.lsEnemy[numEnemy].transform.rotation);
        res.transform.SetParent(parent.transform, false);
        return res;
    }
}
