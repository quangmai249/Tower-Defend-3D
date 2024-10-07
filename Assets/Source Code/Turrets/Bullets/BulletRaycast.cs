using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class BulletRaycast
{
    public static void Shooting(Vector3 pos, Vector3 distance, float damage, bool isLaser)
    {
        EnemyManager e;
        RaycastHit hit;
        Physics.Raycast(pos, distance, out hit);
        if (hit.collider != null)
        {
            e = hit.collider.gameObject.GetComponent<EnemyManager>();
            if (e != null)
            {
                e.SetEnemyHP(-damage);
                if (isLaser == true)
                {
                    e.SetLightLaser(true);
                    e.StartCoroutine(nameof(e.InActiveLaser));
                }
            }
        }
    }
}
