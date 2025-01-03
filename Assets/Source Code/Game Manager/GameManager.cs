using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
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
    private GameStats gameStats;
    private void Awake()
    {
        this.gameStats = new GameStats(this.gold, this.lives, this.waveStart, this.maxWave);

        //LevelDesign level = LevelDesign.Instance;
        //if (this.SetStatsLevel(level.GetLevel()) != null)
        //    this.GameStats = SetStatsLevel(level.GetLevel());

        if (Instance != null)
        {
            Debug.LogError($"{this.gameObject.name} is NOT SINGLE!");
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        //LevelDesign level = LevelDesign.Instance;
        //if (this.SetStatsLevel(level.GetLevel()) != null)
        //    this.GameStats = SetStatsLevel(level.GetLevel());

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
    private GameStats SetStatsLevel(string s)
    {
        switch (s)
        {
            case "LEVEL_1":
                return new GameStats(2000, 10, 1, 2);
            case "LEVEL_2":
                return new GameStats(2500, 15, 1, 3);
            case "LEVEL_3":
                return new GameStats(5000, 20, 1, 4);
            case "LEVEL_4":
                return new GameStats(10000, 100, 1, 3);
            case "LEVEL_5":
                return new GameStats(10000, 100, 1, 3);
            case "LEVEL_6":
                return new GameStats(10000, 100, 1, 3);
            case "LEVEL_7":
                return new GameStats(10000, 100, 1, 3);
            default:
                break;
        }
        return null;
    }

    public bool IsGameWinLevel { get => this.isGameWinLevel; set => this.isGameWinLevel = value; }
    public bool IsGameOver { get => this.isGameOver; set => this.isGameOver = value; }
    public bool IsGamePause { get => this.isGamePause; set => isGamePause = value; }
    public int GameSpeed { get => this.gameMaxSpeed; }
    public GameStats GameStats { get => this.gameStats; set => this.gameStats = value; }
}
