using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Stats")]
    [SerializeField] int waveStart = 1;
    [SerializeField] int lives = 3;
    [SerializeField] float gold = 800;
    [SerializeField] bool isGameOver = false;

    private GameStats gameStats;
    public static GameManager Instance;
    private void Awake()
    {
        this.gameStats = new GameStats(this.gold, this.lives, this.waveStart);
        if (Instance != null)
        {
            Debug.LogError($"{this.gameObject.name} is NOT SINGLE!");
            return;
        }
        Instance = this;
    }
    private void Update()
    {
        if (this.gameStats.GetGold() <= 0)
            this.gameStats.SetGold(0);

        if (this.gameStats.GetLives() <= 0)
        {
            this.gameStats.SetLives(0);
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
