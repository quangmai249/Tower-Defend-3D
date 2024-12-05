using DG.Tweening.Core.Easing;
using UnityEngine;

public class BulletGatelingGun : MonoBehaviour
{
    [SerializeField] float fireCountdown = 1f;
    private LookAtTarget lookAtTarget;
    private TurretStats turretStats;
    private void Start()
    {
        this.turretStats = this.gameObject.transform.GetComponent<Turrets>().GetTurretStats();
        this.lookAtTarget = this.gameObject.GetComponent<LookAtTarget>();
        this.fireCountdown = this.turretStats.RateTurret;
    }
    void Update()
    {
        if (lookAtTarget.IsActiveEffects() == false)
            return;

        this.turretStats = this.gameObject.GetComponent<Turrets>().GetTurretStats();
        this.StartShooting();
    }
    private void StartShooting()
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
}
