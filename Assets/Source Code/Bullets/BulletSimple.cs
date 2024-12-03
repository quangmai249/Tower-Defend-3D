using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BulletSimple : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] float fireCountdown = 1f;

    [Header("Enemies")]
    [SerializeField] GameObject target;
    [SerializeField] readonly string enemyTag = "Enemy";

    private GameManager gameManager;
    private TurretStats turretStats;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    private void Start()
    {
        this.turretStats = this.gameObject.GetComponent<Turrets>().GetTurretStats();
        this.fireCountdown = this.turretStats.RateTurret;
    }
    void Update()
    {
        this.turretStats = this.gameObject.GetComponent<Turrets>().GetTurretStats();
        this.target = SelectTarget.StartSelectTarget(this.gameObject.transform.position, turretStats.RangeTurret, this.enemyTag);

        if (this.target == null || gameManager.IsGameOver == true || gameManager.IsGameWinLevel == true)
            return;
        else
        {
            this.gameObject.transform.LookAt(this.target.transform.position);
            StartSpawnBullet();
        }
    }
    private void StartSpawnBullet()
    {
        this.fireCountdown -= Time.deltaTime;
        if (this.fireCountdown <= 0)
        {
            BulletRaycast.Shooting(this.gameObject.transform.position
                , this.turretStats.RangeTurret * this.gameObject.transform.forward
                , this.turretStats.DamagedTurret, false);

            this.fireCountdown = this.turretStats.RateTurret;
        }
    }
    public GameObject GetTarget()
    {
        return this.target;
    }
}