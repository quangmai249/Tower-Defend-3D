using UnityEngine;

public class BulletCannon : MonoBehaviour
{
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
