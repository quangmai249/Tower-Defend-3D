using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHomeManager : MonoBehaviour
{
    public void ButtonPlay()
    {
        SceneManager.LoadScene("Level Scene");
        return;
    }
    public void ButtonDesignLevel()
    {
        SceneManager.LoadScene("Scene Design Level");
        return;
    }
    public void ButtonEditLevel()
    {
        SceneManager.LoadScene("Scene Edit Level");
        return;
    }
    public void ExitGame()
    {
        Application.Quit();
        return;
    }
}
