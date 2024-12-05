using System.Collections;
using UnityEngine;

public class DoubleBarrelGunEffects : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem parL;
    [SerializeField] ParticleSystem parR;
    [SerializeField] ParticleSystem par_target;
    [SerializeField] AudioSource audioSource;
    private LookAtTarget lookAtTarget;
    private void Start()
    {
        lookAtTarget = this.gameObject.GetComponent<LookAtTarget>();
    }
    private void Update()
    {
        if (lookAtTarget.IsActiveEffects() == false)
        {
            if (animator.GetBool("IsAttack") == true)
                StartCoroutine(nameof(this.CoroutineAniStopAttack));
            return;
        }

        if (animator.GetBool("IsAttack") == false)
        {
            par_target.transform.position = lookAtTarget.GetTarget().transform.position;
            StartCoroutine(nameof(this.CoroutineAniStartAttack));
        }
    }
    IEnumerator CoroutineAniStartAttack()
    {
        yield return new WaitForEndOfFrame();
        animator.SetBool("IsAttack", true);
        //
        parL.Play();
        parR.Play();
        //
        par_target.Play();
        //
        audioSource.Play();
    }
    IEnumerator CoroutineAniStopAttack()
    {
        yield return new WaitForEndOfFrame();
        parL.Stop();
        parR.Stop();
        //
        par_target.Stop();
        par_target.transform.position = this.gameObject.transform.position;
        //
        audioSource.Stop();
        animator.SetBool("IsAttack", false);
    }
}
