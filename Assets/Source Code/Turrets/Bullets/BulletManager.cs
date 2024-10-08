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
    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject go = Instantiate(particleBulletEffect, other.transform);
            go.transform.parent = this.gameObject.transform;

            yield return new WaitForSeconds(.5f);

            DOTween.Kill(this.gameObject.transform);
            Destroy(this.gameObject);
        }
    }
}
