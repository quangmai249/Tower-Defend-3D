using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> lsAudioOST;
    [SerializeField] AudioSource audioSelling;
    [SerializeField] AudioSource audioBuilding;
    [SerializeField] AudioSource audioSpawnEnemy;
    [SerializeField] AudioSource audioProtectedBaseTakeDamage;

    private AudioSource audioOST;
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
