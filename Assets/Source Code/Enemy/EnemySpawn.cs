using System.Collections;
using TMPro;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] TextMeshProUGUI textCountdown;
    [SerializeField] TextMeshProUGUI textWave;

    [Header("Enemy")]
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject listEnemy;

    [Header("Countdown")]
    [SerializeField] int secondStartCountdown = 1;
    [SerializeField] int secondCountdownPerWave = 5;

    [Header("Wave")]
    [SerializeField] int wave = 1;
    [SerializeField] int maxWave = 5;

    [Header("Spawn Enemy")]
    [SerializeField] int quantity = 1;
    [SerializeField] float timeSpawn = 1.5f;

    [SerializeField] readonly string pathManagerTag = "Path Manager";
    private PathManager pathManager;
    private SingletonEnemy singletonEnemy;
    void Start()
    {
        singletonEnemy = SingletonEnemy.Instance;

        pathManager = GameObject
            .FindGameObjectWithTag(pathManagerTag)
            .GetComponent<PathManager>();

        StartCoroutine(nameof(StartCountdown));
    }
    void Update()
    {
        if (secondStartCountdown < 0 && wave < maxWave + 1)
        {
            StartCoroutine(nameof(StartSpawn));
            secondStartCountdown = secondCountdownPerWave;
            StartCoroutine(nameof(StartCountdown));
        }
        SetTextCountdown();
    }
    void SetTextCountdown()
    {
        if (wave > maxWave)
        {
            textCountdown.text = "";
            textWave.text = $"Last wave";
        }
        else
        {
            textCountdown.text = secondStartCountdown.ToString();
        }
    }
    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(secondStartCountdown);

        for (int i = 0; i < quantity; i++)
        {
            singletonEnemy.InstantiateTurretsAt(pathManager.GetPosSpawnEneny(), this.gameObject);
            yield return new WaitForSeconds(timeSpawn);
        }
        quantity++;
    }
    IEnumerator StartCountdown()
    {
        textWave.text = $"Wave {wave} is coming...";
        while (secondStartCountdown >= 0)
        {
            yield return new WaitForSeconds(1);
            secondStartCountdown--;
        }
        wave++;
    }
}
