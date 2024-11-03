using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelection : MonoBehaviour
{
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
