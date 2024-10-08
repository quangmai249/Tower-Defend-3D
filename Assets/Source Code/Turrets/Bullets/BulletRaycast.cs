using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class BulletRaycast
{
    public static void Shooting(GameObject pos, Vector3 distance, float damage, bool isLaser)
    {
        EnemyManager enemyManager;
        RaycastHit hit;
        Physics.Raycast(pos.transform.position, distance, out hit);
        if (hit.collider != null)
        {
            enemyManager = hit.collider.gameObject.GetComponent<EnemyManager>();
            if (enemyManager != null)
            {
                enemyManager.SetEnemyHP(-damage);
            }
        }
    }
}