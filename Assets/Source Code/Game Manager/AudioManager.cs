using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] List<AudioClip> lsAudioOST;
    [SerializeField] AudioSource audioSelling;
    [SerializeField] AudioSource audioBuilding;
    [SerializeField] AudioSource audioSpawnEnemy;
    [SerializeField] AudioSource audioProtectedBaseTakeDamage;

    [Header("Stats")]
    [SerializeField] float volumeMusic = 0.3f;
    [SerializeField] float volumeFXSound = 0.5f;

    private AudioSource audioOST;
    private UIManager uiManager;

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
        uiManager = UIManager.Instance;

        this.audioOST = GetComponent<AudioSource>();
        this.audioOST.resource = this.lsAudioOST.First();
        this.audioOST.Play();
    }
    private void Update()
    {
        if (this.audioOST.isPlaying == false)
        {
            this.audioOST.resource = lsAudioOST[Random.Range(0, lsAudioOST.Count)];
            this.audioOST.Play();
        }

        this.audioOST.volume = this.volumeMusic;
        this.audioBuilding.volume = this.volumeFXSound;
        this.audioSelling.volume = this.volumeFXSound;
        this.audioSpawnEnemy.volume = this.volumeFXSound;
        this.audioProtectedBaseTakeDamage.volume = this.volumeFXSound;
    }
    public void ButtonTest()
    {
        Debug.Log("Button Test");
    }
    public void ButtonSaveSettingAudio()
    {
        if (uiManager.ToggleMusic == false)
            this.volumeMusic = 0f;
        if (uiManager.ToggleFXSound == false)
            this.volumeFXSound = 0f;

        if (uiManager.ToggleMusic == true && this.volumeMusic == 0)
            this.volumeMusic = 0.3f;
        if (uiManager.ToggleFXSound == true && this.volumeFXSound == 0)
            this.volumeFXSound = 0.5f;

        uiManager.ButtonExitSetting();
    }
    public float VolumeMusic { get => this.volumeMusic; set => this.volumeMusic = value; }
    public float VolumeFXSound { get => this.volumeFXSound; set => this.volumeFXSound = value; }
    public void ButtonIncreaseVolumeMusic()
    {
        this.VolumeMusic += 0.1f;

        if (this.VolumeMusic >= 1f)
            this.VolumeMusic = 1f;

        uiManager.SetToggleMusic(true);
    }
    public void ButtonDecreaseVolumeMusic()
    {
        this.VolumeMusic -= 0.1f;
        if (VolumeMusic < 0.1f)
        {
            VolumeMusic = 0f;
            uiManager.SetToggleMusic(false);
        }
    }
    public void ButtonIncreaseVolumeFXSound()
    {
        this.VolumeFXSound += 0.1f;

        if (this.VolumeFXSound >= 1f)
            this.VolumeFXSound = 1f;

        uiManager.SetToggleFXSound(true);
    }
    public void ButtonDecreaseVolumeFXSound()
    {
        this.VolumeFXSound -= 0.1f;
        if (VolumeFXSound < 0.1f)
        {
            VolumeFXSound = 0f;
            uiManager.SetToggleFXSound(false);
        }
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
