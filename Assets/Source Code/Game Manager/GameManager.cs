using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GameStatsLevel
{
    [Header("Game Stats")]
    [SerializeField] int waveStart = 1;
    [SerializeField] int maxWave = 10;
    [SerializeField] int lives = 10;
    [SerializeField] float gold = 1000;

    [Header("Game Speed")]
    [SerializeField] int gameMaxSpeed = 10;

    [Header("Game Controller")]
    [SerializeField] bool isGameWinLevel = false;
    [SerializeField] bool isGameOver = false;
    [SerializeField] bool isGamePause = false;

    public static GameManager Instance;
    private Level level;
    private GameStats gameStats;
    private LevelDesign levelDesign;
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
        levelDesign = LevelDesign.Instance;
        if (levelDesign != null)
            this.SetGameStats();

        this.isGameOver = false;
        this.isGamePause = false;
        this.isGameWinLevel = false;
        Time.timeScale = 1;
    }
    private void SetGameStats()
    {
        if (base.SetStatsLevel(this.levelDesign.GetLevel()) != null)
        {
            this.GameStats.Gold = base.SetStatsLevel(this.levelDesign.GetLevel()).Gold;
            this.GameStats.Lives = base.SetStatsLevel(this.levelDesign.GetLevel()).Lives;
            this.GameStats.WaveStart = base.SetStatsLevel(this.levelDesign.GetLevel()).WaveStart;
            this.GameStats.MaxWave = base.SetStatsLevel(this.levelDesign.GetLevel()).MaxWave;
        }
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
    public bool IsGamePause { get => this.isGamePause; set => isGamePause = value; }
    public int GameSpeed { get => this.gameMaxSpeed; }
    public GameStats GameStats { get => this.gameStats; set => this.gameStats = value; }
}
