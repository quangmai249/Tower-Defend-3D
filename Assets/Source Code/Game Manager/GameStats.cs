using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats
{
    private float gold;
    private int lives;
    private int waveStart;
    private int maxWave;

    public GameStats(float gold, int lives, int waveStart, int maxWave)
    {
        this.gold = gold;
        this.lives = lives;
        this.waveStart = waveStart;
        this.maxWave = maxWave;
    }
    public float Gold { get => gold; set => gold = value; }
    public int Lives { get => lives; set => lives = value; }
    public int WaveStart { get => waveStart; set => waveStart = value; }
    public int MaxWave { get => maxWave; set => maxWave = value; }
}
