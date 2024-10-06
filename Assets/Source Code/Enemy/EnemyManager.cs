using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] float enemyHP = 100f;
    [SerializeField] GameObject parEnemyDeath;
    private GameObject parDeath;
    private GameManager gameManager;

    private void Awake()
    {
        this.gameManager = GameManager.Instance;
    }
    private void Update()
    {
        if (enemyHP == 0)
        {
            this.gameManager.SetGold(100);

            this.parDeath = Instantiate(this.parEnemyDeath.gameObject, this.gameObject.transform.position, this.parEnemyDeath.transform.rotation);
            this.parDeath.transform.parent = this.gameObject.transform.parent.transform;
            Destroy(this.parDeath.gameObject, 2f);

            DOTween.Kill(this.gameObject.transform);
            Destroy(this.gameObject);
        }
    }
    public void SetEnemyHP(float t)
    {
        this.enemyHP += t;
    }
}
