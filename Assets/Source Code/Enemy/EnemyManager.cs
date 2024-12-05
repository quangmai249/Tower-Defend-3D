using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] float defaultHP = 100f;
    [SerializeField] float enemySpeed = 60f;
    [SerializeField] float goldReward = 100f;
    [SerializeField] Image imgHPBar;
    [SerializeField] Image imgHPBarFade;
    [SerializeField] Image imgHPBarBackground;
    [SerializeField] GameObject parEnemyDeath;

    private GameObject parDeath;
    private GameManager gameManager;
    private GameStats gameStats;

    private EnemyStats enemyStats;
    private void Awake()
    {
        this.enemyStats = new EnemyStats(this.defaultHP, this.enemySpeed, this.goldReward);
        gameManager = GameManager.Instance;
        gameStats = gameManager.GameStats;
    }
    private void Start()
    {
        this.SetDefaultHPEnemy();
    }
    private void Update()
    {
        this.imgHPBar.gameObject.transform.rotation = Quaternion.Euler(75f, 0f, 0f);
        this.imgHPBarFade.gameObject.transform.rotation = Quaternion.Euler(75f, 0f, 0f);
        this.imgHPBarBackground.gameObject.transform.rotation = Quaternion.Euler(75f, 0f, 0f);

        if (this.enemyStats.EnemyHP <= 0)
        {
            gameStats.Gold += this.enemyStats.EnemyRewardGold;

            this.parDeath = Instantiate(this.parEnemyDeath.gameObject, this.gameObject.transform.position, this.parEnemyDeath.transform.rotation);
            this.parDeath.transform.parent = this.gameObject.transform.parent.transform;
            Destroy(this.parDeath.gameObject, 2f);
            this.gameObject.SetActive(false);
        }
    }
    public EnemyStats GetEnemyStats()
    {
        return this.enemyStats;
    }
    public void SetEnemyHP(float t)
    {
        this.enemyStats.EnemyHP += t;
        this.imgHPBar.fillAmount = this.enemyStats.EnemyHP / this.defaultHP;

        StartCoroutine(nameof(ChangeImageHPBarFade));

        if (this.enemyStats.EnemyHP <= this.defaultHP / 4)
        {
            this.imgHPBar.color = Color.red;
            return;
        }
        else if (this.enemyStats.EnemyHP <= this.defaultHP / 2)
        {
            this.imgHPBar.color = Color.yellow;
            return;
        }
        return;
    }
    public void SetDefaultHPEnemy()
    {
        this.enemyStats.EnemyHP = this.defaultHP;

        this.imgHPBar.color = Color.green;
        this.imgHPBar.fillAmount = this.enemyStats.EnemyHP / this.defaultHP;

        this.imgHPBarFade.color = Color.white;
        this.imgHPBarFade.fillAmount = this.enemyStats.EnemyHP / this.defaultHP;

        this.imgHPBarBackground.color = Color.black;
        this.imgHPBarBackground.fillAmount = this.enemyStats.EnemyHP / this.defaultHP;
    }
    private IEnumerator ChangeImageHPBarFade()
    {
        float res = this.enemyStats.EnemyHP / this.defaultHP;
        yield return new WaitForSeconds(1f);
        this.imgHPBarFade.fillAmount = res;
    }
}
