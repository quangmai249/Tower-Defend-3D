using UnityEngine;

public class BulletCannon : MonoBehaviour
{
    private LookAtTarget lookAtTarget;
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    private void Start()
    {
        this.lookAtTarget = this.gameObject.GetComponent<LookAtTarget>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TurretStats turretStats = this.gameObject.transform.parent.GetComponent<Turrets>().GetTurretStats();
            other.gameObject.GetComponent<EnemyManager>().SetEnemyHP(-turretStats.DamagedTurret);
            this.gameObject.SetActive(false);
        }
    }
}
