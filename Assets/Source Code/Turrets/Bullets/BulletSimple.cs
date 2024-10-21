using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BulletSimple : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] float fireCountdown = 1f;
    [SerializeField] float defaultFireCountdown = 1f;

    [Header("Enemies")]
    [SerializeField] GameObject target;
    [SerializeField] readonly string enemyTag = "Enemy";

    private GameManager gameManager;
    private TurretStats turretStats;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    void Update()
    {
        this.turretStats = this.gameObject.GetComponent<Turrets>().GetTurretStats();
        this.target = SelectTarget.StartSelectTarget(this.gameObject.transform.position, turretStats.RangeTurret, this.enemyTag);
        if (this.target == null || gameManager.GetIsGameOver() == true)
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
            BulletRaycast.Shooting(this.gameObject, (this.target.transform.position - this.gameObject.transform.position)
                , this.turretStats.DamagedTurret, false);
            this.fireCountdown = defaultFireCountdown;
        }
        this.fireCountdown -= Time.deltaTime;
    }
}