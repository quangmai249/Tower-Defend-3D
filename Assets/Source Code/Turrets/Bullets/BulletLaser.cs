using UnityEngine;

public class BulletLaser : MonoBehaviour
{
    [SerializeField] float damageTurretsLaser = 0.25f;
    [SerializeField] float range = 6f;

    [Header("Enemies")]
    [SerializeField] GameObject target;
    [SerializeField] readonly string enemyTag = "Enemy";

    private GameObject[] enemies;
    private LineRenderer lineRenderer;
    private void Awake()
    {
        this.lineRenderer = GetComponent<LineRenderer>();
        this.lineRenderer.positionCount = 2;
    }
    void Start()
    {
        this.lineRenderer.enabled = false;
    }
    void Update()
    {
        target = SelectTarget.StartSelectTarget(this.enemies, this.gameObject.transform.position, this.range, this.enemyTag);

        if (this.target == null)
        {
            this.lineRenderer.enabled = false;
            return;
        }
        else
        {
            this.gameObject.transform.LookAt(this.target.transform.position);

            this.lineRenderer.enabled = true;
            this.lineRenderer.SetPosition(0, this.gameObject.transform.position);
            this.lineRenderer.SetPosition(1, this.target.transform.position);
            BulletRaycast.Shooting(this.gameObject.transform.position
                , (this.target.transform.position - this.gameObject.transform.position)
                , this.damageTurretsLaser, true);
        }
    }
}
