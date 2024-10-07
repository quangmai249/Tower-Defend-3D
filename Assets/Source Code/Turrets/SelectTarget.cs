using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectTarget
{
    public static GameObject StartSelectTarget(GameObject[] lsEnemies, Vector3 posOrigin, float range, string enemyTag)
    {
        lsEnemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject item in lsEnemies)
        {
            if (Vector3.Distance(posOrigin, item.transform.position) < range)
            {
                return item;
            }
        }
        return null;
    }
}
