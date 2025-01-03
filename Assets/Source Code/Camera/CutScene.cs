using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject Cam01;
    [SerializeField] GameObject Cam02;
    [SerializeField] GameObject Cam03;
    [SerializeField] GameObject buttonSkip;

    [Header("Audio")]
    [SerializeField] AudioClip audio01;
    [SerializeField] AudioClip audio02;
    [SerializeField] AudioClip audio03;

    private AudioSource audioSource;
    private Setting setting;
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();

        this.setting = JsonUtility.FromJson<Setting>(PlayerPrefs.GetString("Setting Game"));
        this.audioSource.volume = this.setting.volumeMusic;

        this.StartCutScene();
    }
    private void StartCutScene()
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
        SceneManager.LoadScene(SceneNameManager.SceneMenuGame);
    }
    IEnumerator CoroutineButtonSkip()
    {
        yield return new WaitForSeconds(8f);
        this.buttonSkip.gameObject.SetActive(true);
    }
    IEnumerator CoroutineCamera()
    {
        this.audioSource.resource = audio01;
        this.audioSource.Play();

        yield return new WaitForSeconds(1f);
        this.Cam01.gameObject.SetActive(false);

        yield return new WaitForSeconds(5.5f);
        this.Cam02.gameObject.SetActive(false);


        yield return new WaitForSeconds(7f);
        this.audioSource.resource = audio02;
        this.audioSource.Play();

        yield return new WaitForSeconds(4f);
        this.audioSource.resource = audio03;
        this.audioSource.Play();

        yield return new WaitForSeconds(1.5f);
        this.audioSource.Stop();
        SceneManager.LoadScene(SceneNameManager.SceneMenuGame);
    }
}
