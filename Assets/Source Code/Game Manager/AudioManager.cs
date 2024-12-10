using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioSource audioSelling;
    [SerializeField] AudioSource audioBuilding;
    [SerializeField] AudioSource audioSpawnEnemy;
    [SerializeField] AudioSource audioProtectedBaseTakeDamage;

    private Setting setting;
    public static AudioManager Instance;
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
        this.setting = JsonUtility.FromJson<Setting>(PlayerPrefs.GetString("Setting Game"));
    }
    private void Update()
    {
        this.audioBuilding.volume = this.setting.volumeFXSound;
        this.audioSelling.volume = this.setting.volumeFXSound;
        this.audioSpawnEnemy.volume = this.setting.volumeFXSound;
        this.audioProtectedBaseTakeDamage.volume = this.setting.volumeFXSound;
    }
    private void FixedUpdate()
    {
        this.setting = JsonUtility.FromJson<Setting>(PlayerPrefs.GetString("Setting Game"));
    }
    public Setting GetValueSetting()
    {
        return this.setting;
    }
    public void ActiveAudioProtectedBaseTakeDamage(bool isPlay)
    {
        if (isPlay == true)
            this.audioProtectedBaseTakeDamage.Play();
        else
            this.audioProtectedBaseTakeDamage.Stop();
    }
    public void ActiveAudioSpawnEnemy(bool isPlay)
    {
        if (isPlay == true)
            this.audioSpawnEnemy.Play();
        else
            this.audioSpawnEnemy.Stop();
    }
    public void ActiveAudioBuilding(bool isPlay)
    {
        if (isPlay == true)
            this.audioBuilding.Play();
        else
            this.audioBuilding.Stop();
    }
    public void ActiveAudioSelling(bool isPlay)
    {
        if (isPlay == true)
            this.audioSelling.Play();
        else
            this.audioSelling.Stop();
    }
}
