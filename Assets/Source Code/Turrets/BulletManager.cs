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
        if (target != null)
        {
            this.gameObject.transform.DOMove(target.transform.position, speedBullet);
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
            Instantiate(particleBulletEffect, other.transform).transform.parent = this.gameObject.transform;
            StartCoroutine(nameof(KillBullets));

            EnemyManager enemy = other.gameObject.GetComponent<EnemyManager>();
            if (enemy != null)
                enemy.SetEnemyHP(-damage);
        }
    }
    private IEnumerator KillBullets()
    {
        yield return new WaitForSeconds(.25f);
        DOTween.Kill(this.gameObject.transform);
        Destroy(this.gameObject);
    }
}
