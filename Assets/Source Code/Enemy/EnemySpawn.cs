using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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

    [Header("Particles")]
    [SerializeField] float yPosEnemyHome = 5f;
    [SerializeField] GameObject enemyHome;
    [SerializeField] GameObject protectedBase;

    [Header("Countdown")]
    [SerializeField] bool isReady = false;
    [SerializeField] int secondStartCountdown = -1;
    [SerializeField] int defaultSecondCountdownPerWave = 3;

    [Header("Wave")]
    [SerializeField] int wave = 1;
    [SerializeField] int maxWave = 20;

    [Header("Spawn Enemy")]
    [SerializeField] float randPath = 1f;
    [SerializeField] float timeSpawn = 1.5f;
    [SerializeField] Vector3 posSpawn;

    [Header("Boss")]
    [SerializeField] GameObject enemyBoss;

    private LevelDesign levelDesign;
    private PathManager pathManager;
    private SingletonEnemy singletonEnemy;
    private GameManager gameManager;
    private GameStats gameStats;
    private AudioManager audioManager;
    void Start()
    {
        this.levelDesign = LevelDesign.Instance;
        this.pathManager = PathManager.Instance;
        this.gameManager = GameManager.Instance;
        this.audioManager = AudioManager.Instance;
        this.singletonEnemy = SingletonEnemy.Instance;

        this.gameStats = gameManager.GameStats;

        this.wave = gameStats.WaveStart;
        this.maxWave = gameStats.MaxWave;

        this.isReady = false;
        this.textWave.text = string.Empty;
        this.textCountdown.text = string.Empty;

        this.btnReadyPlayGame.gameObject.SetActive(false);

        StartCoroutine(nameof(this.CoroutineSpawnBaseConstructor));
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
    IEnumerator CoroutineSpawnBaseConstructor()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < pathManager.GetListFileNodePath().Count; i++)
        {
            GameObject protectedBase = Instantiate(this.protectedBase);
            Vector3 pos = new Vector3(pathManager.GetListFileNodePath()[i].ReadFromFile().Last().x, protectedBase.transform.position.y, pathManager.GetListFileNodePath()[i].ReadFromFile().Last().z);
            protectedBase.transform.parent = this.gameObject.transform;
            protectedBase.transform.position = pos;
        }

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < pathManager.GetListFileNodePath().Count; i++)
        {
            GameObject enemyHome = Instantiate(this.enemyHome);
            Vector3 pos = pathManager.GetListFileNodePath()[i].ReadFromFile().First() + (Vector3.up * yPosEnemyHome);
            enemyHome.transform.parent = this.gameObject.transform;
            enemyHome.transform.position = new Vector3(pos.x, pos.y * 10f, pos.z);
            enemyHome.transform.DOMove(pos, 5f);
        }

        yield return new WaitForSeconds(5f);
        this.btnReadyPlayGame.gameObject.SetActive(true);
    }
    IEnumerator CoroutineChooseEnemyBoss()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < pathManager.GetListFileNodePath().Count; i++)
        {
            this.posSpawn = pathManager.GetListFileNodePath()[i].ReadFromFile().First() + RandomVector3Path(this.randPath);
            this.enemyBoss = singletonEnemy.InstantiateBossAt(posSpawn, levelDesign.GetLevelTypeInt());

            if (enemyBoss != null)
            {
                this.enemyBoss.GetComponent<EnemyMoving>().SetArrayPoint(new FilePath(pathManager.GetPath(), levelDesign.GetLevel() + 1));
                this.SetDefaultEnemy(this.enemyBoss, this.posSpawn);

                audioManager.ActiveAudioSpawnEnemy(true);
                yield return new WaitForSeconds(1f);
            }
        }
    }
    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(secondStartCountdown);

        for (int i = 0; i < this.wave; i++)
        {
            if (this.wave <= 3)
            {
                this.posSpawn = pathManager.GetListFileNodePath().First().ReadFromFile().First() + RandomVector3Path(this.randPath);

                GameObject temp = singletonEnemy.InstantiateEnemysAt(this.posSpawn);
                temp.GetComponent<EnemyMoving>().SetArrayPoint(new FilePath(pathManager.GetPath(), levelDesign.GetLevel() + 1));
                this.SetDefaultEnemy(temp, this.posSpawn);

                audioManager.ActiveAudioSpawnEnemy(true);
            }
            else
            {
                for (int j = 0; j < pathManager.GetListFileNodePath().Count; j++)
                {
                    this.posSpawn = pathManager.GetListFileNodePath()[j].ReadFromFile().First() + RandomVector3Path(this.randPath);

                    GameObject temp = singletonEnemy.InstantiateEnemysAt(this.posSpawn);
                    temp.GetComponent<EnemyMoving>().SetArrayPoint(new FilePath(pathManager.GetPath(), levelDesign.GetLevel() + (j + 1)));
                    this.SetDefaultEnemy(temp, this.posSpawn);

                    audioManager.ActiveAudioSpawnEnemy(true);
                }
            }
            yield return new WaitForSeconds(timeSpawn);
        }

        if (this.wave > this.maxWave)
            StartCoroutine(nameof(this.CoroutineChooseEnemyBoss));
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
