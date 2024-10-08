using UnityEngine;

public class BulletSimple : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] GameObject bullet;
    [SerializeField] float damage = 10f;
    [SerializeField] float fireCountdown = 1f;
    [SerializeField] float defaultFireCountdown = 1f;
    [SerializeField] float range = 6f;

    [Header("Enemies")]
    [SerializeField] GameObject target;
    [SerializeField] readonly string enemyTag = "Enemy";

    private GameObject[] enemies;
    void Update()
    {
        target = SelectTarget.StartSelectTarget(this.enemies, this.gameObject.transform.position, this.range, this.enemyTag);
        if (this.target == null)
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
            CheckGameObjectIsNotNull(this.bullet);
            SpawnBullet();
            BulletRaycast.Shooting(this.gameObject, (this.target.transform.position - this.gameObject.transform.position)
                , this.damage, false);
            this.fireCountdown = defaultFireCountdown;
        }
        this.fireCountdown -= Time.deltaTime;
    }
    private void SpawnBullet()
    {
        GameObject go = Instantiate(this.bullet.gameObject, this.gameObject.transform.position, this.bullet.transform.rotation);
        go.gameObject.transform.parent = this.gameObject.transform;

        BulletManager bulletManager = go.GetComponent<BulletManager>();
        if (bulletManager != null)
            bulletManager.SetTarget(target);
    }
    private void CheckGameObjectIsNotNull(GameObject g)
    {
        if (g == null)
        {
            Debug.LogError($"{g.name} is not null!");
            return;
        }
    }
}
