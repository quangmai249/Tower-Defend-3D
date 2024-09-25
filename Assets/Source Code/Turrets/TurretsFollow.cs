using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

public class TurretsFollow : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] GameObject bullet;
    [SerializeField] float fireCountdown = 1f;
    [SerializeField] float defaultFireCountdown = 1f;

    [Header("Turrets")]
    [SerializeField] float range = 6f;
    [SerializeField] float distance;

    [Header("Enemies")]
    [SerializeField] string enemyTag = "Enemy";
    [SerializeField] GameObject target;

    private GameObject[] enemies;
    private void Start()
    {
        StartCoroutine(nameof(SelectTarget));
    }
    private void Update()
    {
        if (target == null)
            return;
        else
        {
            gameObject.transform.LookAt(target.transform.position);
            StartSpawnBullet();
        }
    }
    private void StartSpawnBullet()
    {
        if (fireCountdown <= 0)
        {
            CheckGameObjectIsNotNull(this.bullet);
            SpawnBullet();
            fireCountdown = defaultFireCountdown;
        }
        fireCountdown -= Time.deltaTime;
    }
    private void SpawnBullet()
    {
        GameObject g = Instantiate(this.bullet, this.gameObject.transform);
        BulletManager bulletManager = g.GetComponent<BulletManager>();
        if (bulletManager != null)
            bulletManager.SetTarget(target);
    }
    private IEnumerator SelectTarget()
    {
        while (true)
        {
            yield return null;
            enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            foreach (GameObject item in enemies)
            {
                distance = Vector3.Distance(gameObject.transform.position, item.transform.position);
                if (distance < range)
                {
                    target = item;
                    break;
                }
                else target = null;
            }
        }
    }
    private void CheckGameObjectIsNotNull(GameObject g)
    {
        if (g == null)
        {
            Debug.LogError($"{g.name} is not null!");
            return;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
    }
}
