using DG.Tweening;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] float enemyHP = 100f;
    void Update()
    {
        if (enemyHP == 0)
        {
            //this.gameObject.transform.DOKill();
            DOTween.Kill(this.gameObject.transform);
            Destroy(this.gameObject);
        }
    }
    public void SetEnemyHP(float t)
    {
        this.enemyHP += t;
    }
}
