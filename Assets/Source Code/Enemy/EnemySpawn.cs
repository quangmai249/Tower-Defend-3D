using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] TextMeshProUGUI textCountdown;

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
    [SerializeField] float timeSpawn = 1f;
    void Start()
    {
        StartCoroutine(nameof(StartCountdown));
    }
    void Update()
    {
        if (secondStartCountdown < 0 && wave < 6)
        {
            StartCoroutine(nameof(StartSpawn));
            secondStartCountdown = secondCountdownPerWave;
            StartCoroutine(nameof(StartCountdown));
        }

        if (wave > maxWave)
            textCountdown.text = "";
        else
            textCountdown.text = secondStartCountdown.ToString();
    }
    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(secondStartCountdown);

        for (int i = 0; i < quantity; i++)
        {
            Instantiate(enemy, gameObject.transform.position, enemy.transform.rotation);
            yield return new WaitForSeconds(timeSpawn);
        }

        quantity++;
        wave++;
    }
    IEnumerator StartCountdown()
    {
        while (secondStartCountdown >= 0)
        {
            yield return new WaitForSeconds(1);
            secondStartCountdown--;
        }
    }
}
