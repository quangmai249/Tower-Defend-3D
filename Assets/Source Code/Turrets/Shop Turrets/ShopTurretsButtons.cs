using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTurretsButtons : MonoBehaviour
{
    SingletonShopTurrets singletonShopTurrets;
    SingletonUpgradeTurrets singletonUpgradeTurrets;
    private void Start()
    {
        singletonShopTurrets = SingletonShopTurrets.Instance;
        singletonUpgradeTurrets = SingletonUpgradeTurrets.Instance;
    }
    private void OnMouseDown()
    {
        if (this.gameObject.name.Equals("Cancel"))
        {
            singletonShopTurrets.SetActiveShopTurrets(false, this.transform.parent.position);
            return;
        }

        if (this.gameObject.name.Equals("Cancel Upgrade"))
        {
            singletonUpgradeTurrets.SetActiveUpgradeTurrets(false, this.transform.parent.position);
            return;
        }
    }
}
