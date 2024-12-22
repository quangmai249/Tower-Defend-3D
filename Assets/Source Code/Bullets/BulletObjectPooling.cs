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
        this.CreateObjectPooling(this.defaultBulletQuantity);
    }
    private void CreateObjectPooling(int defaultQuantity)
    {
        for (int i = 0; i < defaultQuantity; i++)
        {
            this._bullet = Instantiate(this.bullet);
            this._bullet.gameObject.transform.SetParent(this.gameObject.transform);
            this._bullet.gameObject.SetActive(false);
            this.lsBulletPooling.Add(this._bullet);
        }
    }
    public GameObject GetBulletPooling(string tag)
    {
        foreach (GameObject item in this.lsBulletPooling)
        {
            if (item.activeSelf == false && item.tag == tag)
                return item;
        }
        this.CreateObjectPooling(this.defaultBulletQuantity);
        return GetBulletPooling(GameObjectTagManager.TagBullet);
    }
}
