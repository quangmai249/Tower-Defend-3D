using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTurretsButtons : MonoBehaviour
{
    SingletonShopTurrets singletonShopTurrets;
    private void Start()
    {
        singletonShopTurrets = SingletonShopTurrets.Instance;
    }
    private void OnMouseDown()
    {
        if (this.gameObject.name.Equals("Cancel"))
        {
            singletonShopTurrets.SetActiveShopTurrets(false, this.transform.parent.position);
            return;
        }
    }
}
