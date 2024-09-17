using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textCountdown;

    [SerializeField] GameObject enemy;

    [SerializeField] int secondCountdown = 5;

    [SerializeField] int wave = 1;

    [SerializeField] int quantity = 1;

    [SerializeField] float timeSpawn = 0.2f;
    void Start()
    {
        StartCoroutine(nameof(StartCountdown));
    }
    void Update()
    {
        if (secondCountdown < 0 && wave < 6)
        {
            StartCoroutine(nameof(StartSpawn));
            secondCountdown = 5;
            StartCoroutine(nameof(StartCountdown));
        }

        if (wave > 5)
            textCountdown.text = "";
        else
            textCountdown.text = secondCountdown.ToString();
    }
    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(secondCountdown);
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
        while (secondCountdown >= 0)
        {
            yield return new WaitForSeconds(1);
            secondCountdown--;
        }
    }
}
