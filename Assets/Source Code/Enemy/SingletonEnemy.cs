using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonEnemy : MonoBehaviour
{
    [SerializeField] List<GameObject> lsEnemy;
    [SerializeField] List<GameObject> enemyLs;

    [SerializeField] int numPool = 20;
    [SerializeField] string enemyTag = "Enemy";

    private GameObject _enemy;
    private float yPos;
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
    private void Start()
    {
        CreateEnemyObjectPooling(this.lsEnemy, this.numPool);
        this.yPos = lsEnemy[0].gameObject.transform.position.y;
    }
    public GameObject InstantiateTurretsAt(float xPos, float zPos)
    {
        GameObject res = this.GetEnemyPooling(this.enemyTag);
        res.transform.position = new Vector3(xPos, this.yPos, zPos);
        res.gameObject.SetActive(true);
        return res;
    }
    private GameObject GetEnemyPooling(string tag)
    {
        foreach (GameObject item in this.enemyLs)
        {
            if (item.activeSelf == false && item.tag == tag)
                return item;
        }

        this.CreateEnemyObjectPooling(this.lsEnemy, this.numPool);
        return this.GetEnemyPooling(this.enemyTag);
    }
    private void CreateEnemyObjectPooling(List<GameObject> lsGo, int defaultQuantity)
    {
        for (int i = 0; i < defaultQuantity; i++)
        {
            foreach (var item in lsGo)
            {
                this._enemy = Instantiate(item);
                this._enemy.gameObject.transform.SetParent(this.gameObject.transform);
                this._enemy.gameObject.SetActive(false);
                this.enemyLs.Add(this._enemy);
            }
        }
    }
}
