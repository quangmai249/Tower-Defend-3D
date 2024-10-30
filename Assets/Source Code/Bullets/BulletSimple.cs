using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BulletSimple : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] float fireCountdown = 1f;
    [SerializeField] float defaultFireCountdown = 1f;
    [SerializeField] string bulletTag = "Bullet";

    [Header("Enemies")]
    [SerializeField] GameObject target;
    [SerializeField] readonly string enemyTag = "Enemy";

    private GameObject _bullet;
    private GameManager gameManager;
    private TurretStats turretStats;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    private void Start()
    {
        this.fireCountdown = defaultFireCountdown;
    }
    void Update()
    {
        this.turretStats = this.gameObject.GetComponent<Turrets>().GetTurretStats();
        this.target = SelectTarget.StartSelectTarget(this.gameObject.transform.position, turretStats.RangeTurret, this.enemyTag);
        if (this.target == null || gameManager.IsGameOver == true)
        {
            return;
        }
        else
        {
            this.gameObject.transform.LookAt(this.target.transform.position);
            StartSpawnBullet();
        }
    }
    private void StartSpawnBullet()
    {
        if (this.fireCountdown <= 0)
        {
            this._bullet = this.gameObject.GetComponent<BulletObjectPooling>().GetBulletPooling(this.bulletTag); ;
            this._bullet.transform.position = this.gameObject.transform.position;
            this._bullet.SetActive(true);

            StartCoroutine(nameof(this.StartDamage), this._bullet);

            this._bullet.gameObject.transform
                .DOMove(this.target.gameObject.transform.position, defaultFireCountdown);

            this.fireCountdown = defaultFireCountdown;
        }
        this.fireCountdown -= Time.deltaTime;
    }
    private IEnumerator StartDamage(GameObject go)
    {
        yield return new WaitForSeconds(this.defaultFireCountdown);
        BulletRaycast.Shooting(this.gameObject.transform.position, this.turretStats.RangeTurret * this.gameObject.transform.forward, this.turretStats.DamagedTurret, false);
        go.gameObject.SetActive(false);
    }
    public float GetFireCountdown()
    {
        return this.fireCountdown;
    }
    public void SetFireCountdown(float defaultFireCountdown)
    {
        this.defaultFireCountdown = defaultFireCountdown;
    }
}