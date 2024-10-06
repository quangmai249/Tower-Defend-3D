using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float goldStart = 1000;
    [SerializeField] int lives = 3;
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"{this.gameObject.name} is NOT SINGLE!");
            return;
        }
        Instance = this;
    }
    private void Update()
    {
        if (this.goldStart <= 0)
        {
            this.goldStart = 0;
        }
        if (this.lives <= 0)
        {
            this.lives = 0;
        }
    }
    public void SetGold(float gold)
    {
        this.goldStart += gold;
    }
    public float GetGold()
    {
        return this.goldStart;
    }
    public void SetLives(int lives)
    {
        this.lives += lives;
    }
    public int GetLives()
    {
        return this.lives;
    }
}
