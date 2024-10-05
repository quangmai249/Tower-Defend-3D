using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
    [SerializeField] readonly string enemyTag = "Enemy";

    [SerializeField] GameObject nodeBuilding;

    private GameObject[] enemies;
    private Renderer rend;
    private Color color;

    private SingletonBuilding singletonBuilding;
    private SingletonUpgradeTurrets singletonUpgradeTurrets;
    private GameManager gameManager;
    private UIManager uiManager;
    private void Awake()
    {
        singletonBuilding = SingletonBuilding.Instance;
        singletonUpgradeTurrets = SingletonUpgradeTurrets.Instance;
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;
    }
    private void Start()
    {
        if (gameManager.GetGold() < GetPriceGameObject())
        {
            GameObject nodeBuilding = singletonBuilding.InstantiateAt(this.gameObject.transform.position);
            nodeBuilding.transform.parent = this.gameObject.transform.parent.transform;
            Destroy(this.gameObject);
            Debug.Log("Not enough money!");
            return;
        }
        else
        {
            gameManager.SetGold(-GetPriceGameObject());
        }

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
        singletonUpgradeTurrets.SetActiveShopTurrets(true, new Vector3(this.gameObject.transform.position.x, -2f, this.transform.position.z));
        return;
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
        GameObject go = Instantiate(this.bullet.gameObject, this.gameObject.transform.position, this.bullet.transform.rotation);
        go.gameObject.transform.parent = this.gameObject.transform;
        BulletManager bulletManager = go.GetComponent<BulletManager>();
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
    private float GetPriceGameObject()
    {
        if (this.gameObject.name.Equals("Turrets Red(Clone)"))
            return 500f;
        else if (this.gameObject.name.Equals("Turrets Yellow(Clone)"))
            return 300f;
        else if (this.gameObject.name.Equals("Turrets Blue(Clone)"))
            return 100f;
        else
            return 0;
    }
}
