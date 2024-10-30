using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPooling : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] List<GameObject> lsBulletPooling;
    [SerializeField] int defaultBulletQuantity = 5;
    private GameObject _bullet;
    private void Awake()
    {
        CreateObjectPooling(this.defaultBulletQuantity);
    }
    private void CreateObjectPooling(int defaultQuantity)
    {
        for (int i = 0; i < defaultQuantity; i++)
        {
            this._bullet = Instantiate(this.bullet);
            this._bullet.gameObject.transform.SetParent(this.gameObject.transform);
            this._bullet.gameObject.SetActive(false);
            lsBulletPooling.Add(this._bullet);
        }
    }
    public GameObject GetBulletPooling(string tag)
    {
        foreach (GameObject item in lsBulletPooling)
        {
            if (item.activeSelf == false && item.tag == tag)
                return item;
        }

        GameObject go = Instantiate(this.bullet);
        go.gameObject.transform.SetParent(this.gameObject.transform);
        go.gameObject.SetActive(false);
        lsBulletPooling.Add(go);
        return go;
    }
}
