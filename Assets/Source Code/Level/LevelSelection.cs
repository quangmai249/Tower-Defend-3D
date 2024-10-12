using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] GameObject buttonPlayGame;
    private void Start()
    {
        this.buttonPlayGame.SetActive(false);
    }
    public void ButtonSelectLevel(int level)
    {
        PlayerPrefs.SetString("LEVEL", "LEVEL_" + level);
        PlayerPrefs.Save();
        this.buttonPlayGame.SetActive(true);
        return;
    }
    public void ButtonPlayGame()
    {
        SceneManager.LoadScene("Game Play");
        return;
    }
    public void ButtonHomeScreen()
    {
        SceneManager.LoadScene("Menu Game");
        return;
    }
}
