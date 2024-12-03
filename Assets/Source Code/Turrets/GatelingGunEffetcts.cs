using System.Collections;
using UnityEngine;

public class GatelingGunEffetcts : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] AudioSource audioSource;

    private void Update()
    {
        target = this.gameObject.GetComponent<BulletSimple>().GetTarget();

        if (target != null && particleSystem.isPlaying == false && animator.GetBool("IsAttack") == false)
        {
            StartCoroutine(nameof(this.CoroutineAniStartAttack));
        }

        if (target == null && animator.GetBool("IsAttack") == true)
        {
            StartCoroutine(nameof(this.CoroutineAniStopAttack));
        }
    }
    IEnumerator CoroutineAniStartAttack()
    {
        yield return new WaitForEndOfFrame();
        animator.SetBool("IsAttack", true);
        particleSystem.Play();
        audioSource.Play();
    }
    IEnumerator CoroutineAniStopAttack()
    {
        yield return new WaitForEndOfFrame();
        particleSystem.Stop();
        audioSource.Stop();
        animator.SetBool("IsAttack", false);
    }
}
