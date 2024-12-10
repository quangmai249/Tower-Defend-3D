using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelection : MonoBehaviour
{
    [SerializeField] GameObject buttonStartPlay;
    [SerializeField] AudioSource audioSource;
    public static LevelSelection Instance;
    private Setting setting;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"{this.gameObject.name} is NOT NULL!");
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
        this.setting = JsonUtility.FromJson<Setting>(PlayerPrefs.GetString("Setting Game"));
        this.audioSource.volume = this.setting.volumeMusic;
        this.buttonStartPlay.SetActive(false);
    }
    public GameObject GetButtonStartPlay()
    {
        return this.buttonStartPlay;
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
