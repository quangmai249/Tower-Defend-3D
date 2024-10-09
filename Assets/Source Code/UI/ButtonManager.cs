using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void ButtonIncreaseSpeed(float speed)
    {
        Time.timeScale = speed;
    }
    public void ButtonDecreaseSpeed()
    {
        Time.timeScale = 1f;
    }
    public void ButtonRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ButtonMenu()
    {
        SceneManager.LoadScene("Menu Game");
    }
    public void ButtonPlay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game Play");
    }
}
