using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] float damage = 25f;
    [SerializeField] float speedBullet = 0.5f;
    [SerializeField] GameObject target;
    [SerializeField] GameObject particleBulletEffect;
    void Update()
    {
        if (this.target != null)
        {
            this.gameObject.transform.DOMove(this.target.transform.position, speedBullet);
        }
        else
        {
            DOTween.Kill(this.gameObject.transform);
            Destroy(this.gameObject);
        }
    }
    public void SetTarget(GameObject _t)
    {
        this.target = _t;
    }
    public void SetSpeedBullet(float s)
    {
        this.speedBullet = s;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject par = Instantiate(particleBulletEffect, other.transform);
            par.transform.parent = this.gameObject.transform;

            StartCoroutine(nameof(KillBullets));

            EnemyManager enemy = other.gameObject.GetComponent<EnemyManager>();

            if (enemy != null)
                enemy.SetEnemyHP(-damage);

            return;
        }
    }
    private IEnumerator KillBullets()
    {
        yield return new WaitForSeconds(speedBullet / 3);
        DOTween.Kill(this.gameObject.transform);
        Destroy(this.gameObject);
    }
}
