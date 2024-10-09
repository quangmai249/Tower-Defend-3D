using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletLaser : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] GameObject objEffectLaser;

    [Header("Enemies")]
    [SerializeField] GameObject target;
    [SerializeField] readonly string enemyTag = "Enemy";

    private GameObject effectLaser;
    private LineRenderer lineRenderer;

    private GameManager gameManager;
    private TurretStats turretStats;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        turretStats = this.gameObject.GetComponent<Turrets>().GetTurretStats();
        this.lineRenderer = GetComponent<LineRenderer>();
        this.lineRenderer.positionCount = 2;
    }
    void Start()
    {
        this.lineRenderer.enabled = false;
        this.effectLaser = Instantiate(this.objEffectLaser);
        this.effectLaser.transform.parent = this.gameObject.transform;
    }
    void Update()
    {
        target = SelectTarget.StartSelectTarget(this.gameObject.transform.position, turretStats.RangeTurret, this.enemyTag);

        if (this.target == null || gameManager.GetIsGameOver() == true)
        {
            this.lineRenderer.enabled = false;

            this.effectLaser.transform.position = this.gameObject.transform.position;
            this.effectLaser.SetActive(false);

            return;
        }
        else
        {
            this.gameObject.transform.LookAt(this.target.transform.position);

            this.lineRenderer.enabled = true;
            this.lineRenderer.SetPosition(0, this.gameObject.transform.position);
            this.lineRenderer.SetPosition(1, this.target.transform.position);

            this.effectLaser.transform.position = this.target.transform.position;
            this.effectLaser.SetActive(true);

            BulletRaycast.Shooting(this.gameObject
                , (this.target.transform.position - this.gameObject.transform.position)
                , turretStats.DamagedTurret, true);

            this.target.gameObject.GetComponent<EnemyMoving>().SetIsSlowing(true);
        }
    }
}
