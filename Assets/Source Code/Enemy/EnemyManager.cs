using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] float goldReward = 100f;
    [SerializeField] float enemyHP = 100f;
    [SerializeField] float defaultHP;
    [SerializeField] Image imgHPBar;
    [SerializeField] Image imgHPBarFade;
    [SerializeField] Image imgHPBarBackground;
    [SerializeField] GameObject parEnemyDeath;
    private GameObject parDeath;
    private GameManager gameManager;
    private GameStats gameStats;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        gameStats = gameManager.GameStats;

        this.defaultHP = this.enemyHP;
    }
    private void Start()
    {
        this.imgHPBar.color = Color.green;
        this.imgHPBar.fillAmount = this.enemyHP / this.defaultHP;

        this.imgHPBarFade.color = Color.white;
        this.imgHPBarFade.fillAmount = this.enemyHP / this.defaultHP;

        this.imgHPBarBackground.color = Color.black;
        this.imgHPBarBackground.fillAmount = this.enemyHP / this.defaultHP;
    }
    private void Update()
    {
        this.imgHPBar.gameObject.transform.rotation = Quaternion.Euler(75f, 0f, 0f);
        this.imgHPBarFade.gameObject.transform.rotation = Quaternion.Euler(75f, 0f, 0f);
        this.imgHPBarBackground.gameObject.transform.rotation = Quaternion.Euler(75f, 0f, 0f);

        if (this.enemyHP <= 0)
        {
            gameStats.Gold += this.goldReward;

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
        this.imgHPBar.fillAmount = this.enemyHP / this.defaultHP;
        StartCoroutine(nameof(ChangeImageHPBarFade));

        if (this.enemyHP <= 25)
        {
            this.imgHPBar.color = Color.red;
            return;
        }
        else if (this.enemyHP <= 50)
        {
            this.imgHPBar.color = Color.yellow;
            return;
        }
        return;
    }
    private IEnumerator ChangeImageHPBarFade()
    {
        float res = this.enemyHP / this.defaultHP;
        yield return new WaitForSeconds(1f);
        this.imgHPBarFade.fillAmount = res;
    }
}
