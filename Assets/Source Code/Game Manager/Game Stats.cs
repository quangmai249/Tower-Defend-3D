using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats
{
    private float gold;
    public void SetGold(float g)
    {
        this.gold += g;
    }
    public float GetGold()
    {
        return this.gold;
    }
}
