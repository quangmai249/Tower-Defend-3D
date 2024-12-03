using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BulletSimple : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] float fireCountdown = 1f;

    [Header("Enemies")]
    [SerializeField] GameObject target;
    [SerializeField] string enemyTag = "Enemy";

    private GameManager gameManager;
    private TurretStats turretStats;
    private bool isGatelingGunShooting;
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
        {
            this.isGatelingGunShooting = false;
            return;
        }
        else
        {
            this.gameObject.transform.LookAt(this.target.transform.position);
            this.StartShooting();
        }
    }
    private void StartShooting()
    {
        this.GatelingGunShooting();
    }
    private void GatelingGunShooting()
    {
        if (isGatelingGunShooting == false)
            return;

        this.fireCountdown -= Time.deltaTime;
        if (this.fireCountdown <= 0)
        {
            BulletRaycast.Shooting(this.gameObject.transform.position
                , this.turretStats.RangeTurret * this.gameObject.transform.forward
                , this.turretStats.DamagedTurret, false);

            this.fireCountdown = this.turretStats.RateTurret;
        }
    }
    public void SetGatelingGunShooting(bool isShoot)
    {
        this.isGatelingGunShooting = isShoot;
    }
    public GameObject GetTarget()
    {
        return this.target;
    }
}