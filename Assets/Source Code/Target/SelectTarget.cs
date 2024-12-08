using System.Collections.Generic;
using UnityEngine;

public static class SelectTarget
{
    public static GameObject StartSelectTarget(Vector3 posOrigin, float range, string enemyTag)
    {
        GameObject[] lsEnemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject item in lsEnemies)
        {
            if (Vector3.Distance(posOrigin, item.transform.position) < range)
            {
                return item;
            }
        }
        return null;
    }
    public static GameObject SelectFirstTargetWithPos(Vector3 pos, string tag)
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag(tag))
            if (g.transform.position == pos)
                return g;
        return null;
    }
    public static void SetActiveGameObjecstWithTag(bool b, string tag)
    {
        foreach (var item in GameObject.FindGameObjectsWithTag(tag))
            item.gameObject.SetActive(b);
    }
    public static GameObject SelectFirstGameObjectWithTag(string tag)
    {
        foreach (var item in GameObject.FindGameObjectsWithTag(tag))
            return item;
        return null;
    }
}
