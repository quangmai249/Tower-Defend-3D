using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    [SerializeField] GameObject Cam01;
    [SerializeField] GameObject Cam02;
    [SerializeField] GameObject Cam03;

    [SerializeField] GameObject buttonSkip;
    void Start()
    {
        this.ResetCutScene();
    }
    private void ResetCutScene()
    {
        this.Cam01.gameObject.SetActive(true);
        this.Cam02.gameObject.SetActive(true);
        this.Cam03.gameObject.SetActive(true);

        this.buttonSkip.gameObject.SetActive(false);

        StartCoroutine(nameof(this.CoroutineCamera));
        StartCoroutine(nameof(this.CoroutineButtonSkip));
    }
    public void ButtonSkip()
    {
        SceneManager.LoadScene("Menu Game");
    }
    IEnumerator CoroutineButtonSkip()
    {
        yield return new WaitForSeconds(8f);
        this.buttonSkip.gameObject.SetActive(true);
    }
    IEnumerator CoroutineCamera()
    {
        yield return new WaitForSeconds(1f);
        this.Cam01.gameObject.SetActive(false);
        yield return new WaitForSeconds(5.5f);
        this.Cam02.gameObject.SetActive(false);
        yield return new WaitForSeconds(13f);
        SceneManager.LoadScene("Menu Game");
    }
}
