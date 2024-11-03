using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] Button btnReadyPlayGame;
    [SerializeField] TextMeshProUGUI textCountdown;
    [SerializeField] TextMeshProUGUI textWave;

    [Header("Countdown")]
    [SerializeField] bool isReady = false;
    [SerializeField] int secondStartCountdown = -1;
    [SerializeField] int defaultSecondCountdownPerWave = 3;

    [Header("Wave")]
    [SerializeField] int wave = 1;
    [SerializeField] int maxWave = 20;

    [Header("Spawn Enemy")]
    [SerializeField] Vector3 posSpawn;
    [SerializeField] float randPath = 1f;
    [SerializeField] float timeSpawn = 1.5f;

    [SerializeField] readonly string levelDesignTag = "Level Design";
    [SerializeField] readonly string pathManagerTag = "Path Manager";
    [SerializeField] readonly string enemyManagerTag = "Enemy Manager";
    [SerializeField] readonly string gameManagerTag = "GameController";

    private LevelDesign levelDesign;
    private PathManager pathManager;
    private SingletonEnemy singletonEnemy;
    private GameManager gameManager;
    private GameStats gameStats;
    private void Awake()
    {
        singletonEnemy = GameObject.FindGameObjectWithTag(this.enemyManagerTag).GetComponent<SingletonEnemy>();
        gameManager = GameObject.FindGameObjectWithTag(this.gameManagerTag).GetComponent<GameManager>();
        levelDesign = GameObject.FindGameObjectWithTag(this.levelDesignTag).GetComponent<LevelDesign>();
        pathManager = GameObject.FindGameObjectWithTag(this.pathManagerTag).GetComponent<PathManager>();
    }
    void Start()
    {
        gameStats = gameManager.GameStats;

        wave = gameStats.WaveStart;
        maxWave = gameStats.MaxWave;

        this.isReady = false;
        textWave.text = string.Empty;
        textCountdown.text = string.Empty;
    }
    void Update()
    {
        if (gameManager.IsGameOver == true || gameManager.IsGameWinLevel == true)
            return;

        if (wave <= maxWave + 1 && this.isReady == true)
        {
            StartCoroutine(nameof(StartCountdown));
            StartCoroutine(nameof(StartSpawn));
            this.isReady = false;
        }

        if (wave == maxWave && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            gameManager.IsGameWinLevel = true;

        SetTextCountdown();
    }
    public void ButtonReadyPlayGame()
    {
        if (gameManager.IsGamePause == true || gameManager.IsGameOver == true || gameManager.IsGameWinLevel == true)
            return;

        this.isReady = true;
        this.btnReadyPlayGame.gameObject.SetActive(false);
        this.secondStartCountdown = this.defaultSecondCountdownPerWave;
    }
    void SetTextCountdown()
    {
        if (wave > maxWave)
        {
            textCountdown.text = "";
            textWave.text = $"Final wave";
        }
        else if (secondStartCountdown >= 0)
        {
            textCountdown.text = secondStartCountdown.ToString();
        }
        else
            textCountdown.text = string.Empty;
    }
    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(secondStartCountdown);

        for (int i = 0; i < this.wave; i++)
        {
            if (this.wave <= 3)
            {
                this.posSpawn = pathManager.GetListFileNodePath().First().ReadFromFile().First() + RandomVector3Path(this.randPath);
                GameObject temp = singletonEnemy.InstantiateTurretsAt(this.posSpawn, this.gameObject);
                temp.GetComponent<EnemyMoving>().SetArrayPoint(new FilePath(pathManager.GetPath(), levelDesign.GetLevel() + 1));
            }
            else
            {
                for (int j = 0; j < pathManager.GetListFileNodePath().Count; j++)
                {
                    this.posSpawn = pathManager.GetListFileNodePath()[j].ReadFromFile().First() + RandomVector3Path(this.randPath); ;
                    GameObject temp = singletonEnemy.InstantiateTurretsAt(this.posSpawn, this.gameObject);
                    temp.GetComponent<EnemyMoving>().SetArrayPoint(new FilePath(pathManager.GetPath(), levelDesign.GetLevel() + (j + 1)));
                }
            }
            yield return new WaitForSeconds(timeSpawn);
        }

        yield return new WaitForSeconds(5f);
        this.btnReadyPlayGame.gameObject.SetActive(true);
    }
    IEnumerator StartCountdown()
    {
        textWave.text = $"Wave {wave}";
        gameStats.WaveStart = wave - 1;

        while (secondStartCountdown >= 0)
        {
            yield return new WaitForSeconds(1);
            secondStartCountdown--;
        }
        wave++;
    }
    private Vector3 RandomVector3Path(float rand)
    {
        return new Vector3(Random.Range(-rand, rand), 0, Random.Range(-rand, rand));
    }
}
