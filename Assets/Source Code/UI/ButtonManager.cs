using DG.Tweening;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject btnIncreaseSpeed;
    [SerializeField] GameObject btnDecreaseSpeed;
    [SerializeField] GameObject btnPauseGame;
    [SerializeField] GameObject btnResumeGame;
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    private void Start()
    {
        gameManager.SetIsGameOver(false);
        gameManager.SetIsGamePause(false);
        this.btnIncreaseSpeed.gameObject.SetActive(true);
        this.btnDecreaseSpeed.gameObject.SetActive(false);
        this.btnPauseGame.gameObject.SetActive(true);
        this.btnResumeGame.gameObject.SetActive(false);
    }
    public void ButtonPauseGame()
    {
        if (gameManager.GetIsGameOver() == true)
            return;

        this.btnPauseGame.gameObject.SetActive(false);
        this.btnResumeGame.gameObject.SetActive(true);
        this.btnIncreaseSpeed.gameObject.SetActive(true);
        this.btnDecreaseSpeed.gameObject.SetActive(false);

        DOTween.PauseAll();
        Time.timeScale = 0f;

        gameManager.SetIsGamePause(true);
        return;
    }
    public void ButtonResumeGame()
    {
        if (gameManager.GetIsGameOver() == true)
            return;

        this.btnPauseGame.gameObject.SetActive(true);
        this.btnResumeGame.gameObject.SetActive(false);
        this.btnIncreaseSpeed.gameObject.SetActive(true);
        this.btnDecreaseSpeed.gameObject.SetActive(false);

        DOTween.PlayAll();
        Time.timeScale = 1.0f;

        gameManager.SetIsGamePause(false);
        return;
    }
    public void ButtonIncreaseSpeed(float speed)
    {
        if (gameManager.GetIsGamePause() == true || gameManager.GetIsGameOver() == true)
            return;

        this.btnIncreaseSpeed.gameObject.SetActive(false);
        this.btnDecreaseSpeed.gameObject.SetActive(true);

        Time.timeScale = speed;
        return;
    }
    public void ButtonDecreaseSpeed(float speed)
    {
        if (gameManager.GetIsGamePause() == true || gameManager.GetIsGameOver() == true)
            return;

        this.btnIncreaseSpeed.gameObject.SetActive(true);
        this.btnDecreaseSpeed.gameObject.SetActive(false);

        Time.timeScale = speed;
        return;
    }
    public void ButtonRestart()
    {
        SceneManager.LoadScene("Game Play");
        return;
    }
    public void ButtonMenu()
    {
        SceneManager.LoadScene("Menu Game");
        return;
    }
}
