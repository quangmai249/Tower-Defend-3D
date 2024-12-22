using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHomeManager : MonoBehaviour
{
    [SerializeField] GameObject panelConfirmQuitGame;
    private void Start()
    {
        this.panelConfirmQuitGame.gameObject.SetActive(false);
    }
    public void ButtonPlay()
    {
        SceneManager.LoadScene(SceneNameManager.SceneLevel);
        return;
    }
    public void ButtonDesignLevel()
    {
        SceneManager.LoadScene(SceneNameManager.SceneDesignLevel);
        return;
    }
    public void ExitGame()
    {
        this.panelConfirmQuitGame.gameObject.SetActive(true);
        return;
    }
    public void ButtonConfirmQuitGame()
    {
        Application.Quit();
        return;
    }
    public void ButtonDontQuitGame()
    {
        this.panelConfirmQuitGame.gameObject.SetActive(false);
        return;
    }
}
