using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats
{
    private float gold;
    private int lives;
    private int waveStart;
    public GameStats() { }
    public GameStats(float gold, int lives, int waveStart)
    {
        this.gold = gold;
        this.lives = lives;
        this.waveStart = waveStart;
    }
    public void SetGold(float g)
    {
        this.gold += g;
    }
    public float GetGold()
    {
        return this.gold;
    }
    public void SetLives(int live)
    {
        this.lives += live;
    }
    public int GetLives()
    {
        return this.lives;
    }
    public void SetWave(int waveStart)
    {
        this.waveStart = waveStart;
    }
    public int GetCurrentWave()
    {
        return this.waveStart;
    }
}
