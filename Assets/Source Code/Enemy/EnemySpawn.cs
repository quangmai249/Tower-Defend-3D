using DG.Tweening;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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

        this.wave = gameStats.WaveStart;
        this.maxWave = gameStats.MaxWave;

        this.isReady = false;
        textWave.text = string.Empty;
        textCountdown.text = string.Empty;
    }
    void Update()
    {
        if (gameManager.IsGameOver == true || gameManager.IsGameWinLevel == true)
            return;

        if (wave <= maxWave && this.isReady == true)
        {
            StartCoroutine(nameof(StartCountdown));
            StartCoroutine(nameof(StartSpawn));
            StartCoroutine(nameof(StartActiveButtonReady));
            this.isReady = false;
        }

        if (wave > maxWave && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            for (int i = 1; i < 100; i++)
            {
                if (PlayerPrefs.GetString("LEVEL").Equals($"LEVEL_{i}"))
                {
                    if (PlayerPrefs.GetInt($"LEVEL_{i + 1}") == 0)
                    {
                        PlayerPrefs.SetInt($"LEVEL_{i + 1}", 1);
                        PlayerPrefs.Save();
                    }
                    break;
                }
            }

            gameManager.IsGameWinLevel = true;
        }

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
            this.btnReadyPlayGame.gameObject.SetActive(false);
        }

        if (secondStartCountdown >= 0)
            textCountdown.text = secondStartCountdown.ToString();
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
                GameObject temp = singletonEnemy.InstantiateTurretsAt(this.posSpawn.x, this.posSpawn.z);
                temp.GetComponent<EnemyMoving>().SetArrayPoint(new FilePath(pathManager.GetPath(), levelDesign.GetLevel() + 1));
                this.SetDefaultEnemy(temp, this.posSpawn);
            }
            else
            {
                for (int j = 0; j < pathManager.GetListFileNodePath().Count; j++)
                {
                    this.posSpawn = pathManager.GetListFileNodePath()[j].ReadFromFile().First() + RandomVector3Path(this.randPath); ;
                    GameObject temp = singletonEnemy.InstantiateTurretsAt(this.posSpawn.x, this.posSpawn.z);
                    temp.GetComponent<EnemyMoving>().SetArrayPoint(new FilePath(pathManager.GetPath(), levelDesign.GetLevel() + (j + 1)));
                    this.SetDefaultEnemy(temp, this.posSpawn);
                }
            }
            yield return new WaitForSeconds(timeSpawn);
        }
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
    IEnumerator StartActiveButtonReady()
    {
        yield return new WaitForSeconds(this.defaultSecondCountdownPerWave + 2f);
        this.btnReadyPlayGame.gameObject.SetActive(true);
    }
    private Vector3 RandomVector3Path(float rand)
    {
        return new Vector3(Random.Range(-rand, rand), 0, Random.Range(-rand, rand));
    }
    private void SetDefaultEnemy(GameObject go, Vector3 posSpawn)
    {
        go.gameObject.transform.position = new Vector3(posSpawn.x, go.transform.position.y, posSpawn.z);
        go.GetComponent<EnemyManager>().SetDefaultHPEnemy();
        go.GetComponent<EnemyMoving>().Moving();
    }
}
