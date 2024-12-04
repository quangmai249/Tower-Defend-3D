using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using UnityEngine;

public class CannonEffects : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem par;
    [SerializeField] AudioSource audioSource;

    [SerializeField] float speed = 0.25f;
    [SerializeField] string bulletTag = "Bullet";
    [SerializeField] GameObject bulletCannon;

    private LookAtTarget lookAtTarget;
    private BulletObjectPooling bulletObjectPooling;
    private void Start()
    {
        lookAtTarget = this.gameObject.GetComponent<LookAtTarget>();
        bulletObjectPooling = this.gameObject.GetComponent<BulletObjectPooling>();
    }
    private void Update()
    {
        if (lookAtTarget.IsActiveEffects() == false)
            return;

        if (animator.GetBool("IsAttack") == false && audioSource.isPlaying == false)
        {
            StartCoroutine(nameof(this.CoroutineAniStartAttack));
            StartCoroutine(nameof(this.CoroutineAniStopAttack));
        }
    }
    IEnumerator CoroutineAniStartAttack()
    {
        yield return new WaitForEndOfFrame();
        this.animator.SetBool("IsAttack", true);
        this.par.Play();
        this.audioSource.Play();

        yield return new WaitForEndOfFrame();
        this.bulletCannon = bulletObjectPooling.GetBulletPooling(this.bulletTag);
        this.bulletCannon.SetActive(true);
        this.bulletCannon.transform.DOMove(lookAtTarget.GetTarget().transform.position, this.speed);
    }
    IEnumerator CoroutineAniStopAttack()
    {
        yield return new WaitForSeconds(GetComponent<Turrets>().GetTurretStats().RateTurret - 0.5f);
        animator.SetBool("IsAttack", false);
    }
}
