using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTurrets : MonoBehaviour
{
    private SingletonUpgradeTurrets singletonUpgradeTurrets;
    private void Start()
    {
        singletonUpgradeTurrets = SingletonUpgradeTurrets.Instance;
    }
    private void OnMouseDown()
    {
        if (this.gameObject.name.Equals("Cancel"))
        {
            singletonUpgradeTurrets.SetActiveShopTurrets(false, this.gameObject.transform.parent.position);
            return;
        }
        else if (this.gameObject.name.Equals("Delete"))
        {
            Debug.Log("Delete");
            return;
        }
        else if (this.gameObject.name.Equals("Upgrade"))
        {
            Debug.Log("Upgrade");
            return;
        }
        singletonUpgradeTurrets.SetActiveShopTurrets(true, this.gameObject.transform.parent.position);
        return;
    }
}
