using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHomeManager : MonoBehaviour
{
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    public void ButtonPlay()
    {
        gameManager.SetIsGameOver(false);
        gameManager.SetIsGamePause(false);
        SceneManager.LoadScene("Game Play");
        return;
    }
    public void ExitGame()
    {
        Application.Quit();
        return;
    }
}
