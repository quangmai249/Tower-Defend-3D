using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Stats")]
    [SerializeField] int waveStart = 1;
    [SerializeField] int maxWave = 10;
    [SerializeField] int lives = 10;
    [SerializeField] float gold = 1000;

    [SerializeField] float timeDurationEnemyMoving = 60f;

    [SerializeField] bool isGameWinLevel = false;
    [SerializeField] bool isGameOver = false;
    [SerializeField] bool isGamePause = false;

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
    private void Start()
    {
        this.isGameOver = false;
        this.isGamePause = false;
        this.isGameWinLevel = false;
        Time.timeScale = 1;
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
    public bool IsGameWinLevel { get => this.isGameWinLevel; set => this.isGameWinLevel = value; }
    public bool IsGameOver { get => this.isGameOver; set => this.isGameOver = value; }
    public bool IsGamePause { get => isGamePause; set => isGamePause = value; }
    public float TimeDurationEnemyMoving { get => timeDurationEnemyMoving; set => timeDurationEnemyMoving = value; }
    public GameStats GameStats { get => this.gameStats; set => this.gameStats = value; }
}
