using System.Collections;
using UnityEngine;

public class LaserGunEffects : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem par;
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

        if (par.isPlaying == false && animator.GetBool("IsAttack") == false)
        {
            par_target.transform.position = lookAtTarget.GetTarget().transform.position;
            StartCoroutine(nameof(this.CoroutineAniStartAttack));
        }
    }
    IEnumerator CoroutineAniStartAttack()
    {
        yield return new WaitForEndOfFrame();
        animator.SetBool("IsAttack", true);
        par.Play();
        //
        par_target.Play();
        //
        audioSource.Play();
    }
    IEnumerator CoroutineAniStopAttack()
    {
        yield return new WaitForEndOfFrame();
        par.Stop();
        //
        par_target.transform.position = this.gameObject.transform.position;
        par_target.Stop();
        //
        audioSource.Stop();
        animator.SetBool("IsAttack", false);
    }
}
