using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretsFollow : MonoBehaviour
{
    [SerializeField] float range = 5f;

    [SerializeField] float distance;

    [SerializeField] string enemyTag = "Enemy";

    [SerializeField] GameObject target;

    private GameObject[] enemies;
    private void Start()
    {
        StartCoroutine(nameof(SelectTarget));
    }
    private void Update()
    {
        if (target != null)
            gameObject.transform.LookAt(target.transform.position);
    }
    IEnumerator SelectTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);

            enemies = GameObject.FindGameObjectsWithTag(enemyTag);

            foreach (GameObject item in enemies)
            {
                distance = Vector3.Distance(gameObject.transform.position, item.transform.position);
                if (distance < range)
                {
                    target = item;
                    break;
                }
                else target = null;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    public void SetRange(float r)
    {
        this.range = r;
    }
}
