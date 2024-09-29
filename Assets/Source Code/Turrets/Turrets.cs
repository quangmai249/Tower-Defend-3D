using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turrets : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] GameObject bullet;
    [SerializeField] float fireCountdown = 1f;
    [SerializeField] float defaultFireCountdown = 1f;

    [Header("Turrets")]
    [SerializeField] float range = 6f;
    [SerializeField] float distance;

    [Header("Enemies")]
    [SerializeField] GameObject target;
    [SerializeField] string enemyTag = "Enemy";

    [SerializeField] GameObject nodeBuilding;

    private GameObject[] enemies;
    private Renderer rend;
    private Color color;
    private SingletonBuilding singletonBuilding;
    private void Start()
    {
        this.singletonBuilding = SingletonBuilding.Instance;
        this.rend = GetComponent<Renderer>();
        this.color = this.rend.material.color;
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
    private void OnMouseEnter()
    {
        rend.material.color = Color.green;
    }
    private void OnMouseExit()
    {
        rend.material.color = this.color;
    }
    private void OnMouseDown()
    {
        singletonBuilding.InstantiateAt(this.gameObject.transform.position);
        DOTween.Kill(this.gameObject.transform);
        Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
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
}
