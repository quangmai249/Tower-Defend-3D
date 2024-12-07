using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> lsAudioOST;
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
}
