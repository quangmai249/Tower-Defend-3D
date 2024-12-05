using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletLaser : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] GameObject objEffectLaser;
    [SerializeField] Vector3 laserPos = new Vector3(0, 1.5f, 0);
    [SerializeField] float timeDurationSlowing = 0.75f;

    private GameObject effectLaser;
    private LineRenderer lineRenderer;

    private TurretStats turretStats;
    private LookAtTarget lookAtTarget;
    private void Awake()
    {
        this.lineRenderer = GetComponent<LineRenderer>();
        this.lineRenderer.positionCount = 2;
    }
    void Start()
    {
        this.lookAtTarget = GetComponent<LookAtTarget>();
        this.lineRenderer.enabled = false;
        this.effectLaser = Instantiate(this.objEffectLaser);
        this.effectLaser.transform.parent = this.gameObject.transform;
    }
    void Update()
    {
        if (lookAtTarget.IsActiveEffects() == false)
        {
            this.lineRenderer.enabled = false;

            this.effectLaser.transform.position = this.gameObject.transform.position;
            this.effectLaser.SetActive(false);

            return;
        }

        if (lookAtTarget.GetTarget() != null)
        {
            this.lineRenderer.enabled = true;
            this.lineRenderer.SetPosition(0, this.gameObject.transform.position + this.laserPos);
            this.lineRenderer.SetPosition(1, lookAtTarget.GetPosTarget());

            this.effectLaser.transform.position = lookAtTarget.GetPosTarget();
            this.effectLaser.SetActive(true);

            this.turretStats = this.gameObject.GetComponent<Turrets>().GetTurretStats();

            BulletRaycast.Shooting(this.gameObject.transform.position
                , (lookAtTarget.GetPosTarget() - this.gameObject.transform.position)
                , this.turretStats.DamagedTurret, true);

            lookAtTarget.GetTarget().gameObject.GetComponent<EnemyMoving>().SetIsSlowing(true, this.timeDurationSlowing);
        }
    }
    public void SetTimeSlowing(float timeSlowing)
    {
        this.timeDurationSlowing = timeSlowing;
    }
    public float GetTimeSlowing()
    {
        return this.timeDurationSlowing;
    }
}
