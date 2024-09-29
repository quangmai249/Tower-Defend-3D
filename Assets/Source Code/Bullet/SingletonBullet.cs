using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBullet : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public static SingletonBullet Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"{this.name} is NOT SINGLE!");
            return;
        }
        Instance = this;
    }
    public GameObject InstantiateBulletAt(Vector3 pos)
    {
        GameObject res = Instantiate(this.bullet, pos, this.bullet.transform.rotation);
        res.transform.parent = this.gameObject.transform;
        return res;
    }
}
