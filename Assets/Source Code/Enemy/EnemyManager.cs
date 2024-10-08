using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] float goldReward = 100f;
    [SerializeField] float enemyHP = 100f;
    [SerializeField] float defaultHP;
    [SerializeField] GameObject parEnemyDeath;
    private GameObject parDeath;
    private GameManager gameManager;
    private void Awake()
    {
        this.gameManager = GameManager.Instance;

        this.defaultHP = this.enemyHP;

    }
    private void Update()
    {
        if (this.enemyHP <= 0)
        {
            this.gameManager.SetGold(this.goldReward);

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
