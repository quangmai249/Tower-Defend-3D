using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void ButtonRetry()
    {
        SceneManager.LoadScene("Game Play");
    }
    public void ButtonMenu()
    {
        SceneManager.LoadScene("Menu Game");
    }
    public void ButtonPlay()
    {
        SceneManager.LoadScene("Game Play");
    }
}
