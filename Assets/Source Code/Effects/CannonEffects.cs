using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CannonEffects : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem par;
    [SerializeField] ParticleSystem par_target;
    [SerializeField] AudioSource audioSource;

    [Header("Stats")]
    [SerializeField] float speed = 0.25f;
    [SerializeField] GameObject bulletCannon;

    private AudioManager audioManager;
    private GameObject target;
    private LookAtTarget lookAtTarget;
    private BulletObjectPooling bulletObjectPooling;
    private void Start()
    {
        lookAtTarget = this.gameObject.GetComponent<LookAtTarget>();
        bulletObjectPooling = this.gameObject.GetComponent<BulletObjectPooling>();
        this.audioManager = AudioManager.Instance;
    }
    private void Update()
    {
        if (lookAtTarget.IsActiveEffects() == false)
            return;

        this.audioSource.volume = this.audioManager.GetValueSetting().volumeFXSound;

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
        this.bulletCannon = bulletObjectPooling.GetBulletPooling(GameObjectTagManager.TagBullet);
        this.bulletCannon.SetActive(true);

        if (lookAtTarget.GetTarget() != null)
            this.bulletCannon.transform.DOMove(lookAtTarget.GetTarget().transform.position, this.speed);

        yield return new WaitForSeconds(this.speed);
        this.par_target.transform.position = lookAtTarget.GetTarget().transform.position;
        this.par_target.Play();
    }
    IEnumerator CoroutineAniStopAttack()
    {
        yield return new WaitForSeconds(GetComponent<Turrets>().GetTurretStats().RateTurret - 0.5f);
        animator.SetBool("IsAttack", false);
    }
}
