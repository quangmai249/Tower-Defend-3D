using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject par = Instantiate(this.particleBulletEffect, other.transform);
            par.transform.parent = this.gameObject.transform;
            StartCoroutine(nameof(KillBullets));
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
