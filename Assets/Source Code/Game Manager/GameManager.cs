using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Stats")]
    [SerializeField] int waveStart = 1;
    [SerializeField] int maxWave = 10;
    [SerializeField] int lives = 3;
    [SerializeField] float gold = 800;
    [SerializeField] bool isGameOver = false;

    public static GameManager Instance;
    private GameStats gameStats;
    private void Awake()
    {
        this.gameStats = new GameStats(this.gold, this.lives, this.waveStart, this.maxWave);
        if (Instance != null)
        {
            Debug.LogError($"{this.gameObject.name} is NOT SINGLE!");
            return;
        }
        Instance = this;
    }
    private void Update()
    {
        if (this.gameStats.Gold <= 0)
            this.gameStats.Gold = 0;

        if (this.gameStats.Lives <= 0)
        {
            this.gameStats.Lives = 0;
            this.isGameOver = true;
        }
    }
    public GameStats GetGameStats()
    {
        return this.gameStats;
    }
    public bool GetIsGameOver()
    {
        return this.isGameOver;
    }
}
