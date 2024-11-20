using Firebase.Extensions;
using Firebase.Storage;
using System;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
public class FirebaseInit : MonoBehaviour
{
    [SerializeField] Slider slideLoading;
    [SerializeField] int maxNodePath = 12;
    [SerializeField] float defaultTimeLoading = 3f;

    [SerializeField] string linkLocal = "C:/Tower Defend 3D";
    [SerializeField] string linkFirebase = "https://firebasestorage.googleapis.com/v0/b/tower-defend-3d-unity-84f17.appspot.com/o/";
    private void Awake()
    {
        WebClient webClient = new WebClient();
        FirebaseStorage firebaseStorage = FirebaseStorage.DefaultInstance;

        this.CheckingNodePath(firebaseStorage, webClient);
        this.CheckingNodeBuilding(firebaseStorage, webClient);
    }
    private void Start()
    {
        slideLoading.value = 0f;
        slideLoading.minValue = 0f;
        slideLoading.maxValue = this.defaultTimeLoading;
    }
    private void Update()
    {
        if (this.slideLoading.value >= this.slideLoading.maxValue)
        {
            return;
        }
        else
            slideLoading.value += Time.deltaTime;
    }
    private void CheckingNodePath(FirebaseStorage firebaseStorage, WebClient webClient)
    {
        string folderFirebase = $"{this.linkFirebase}FileNodePath%2F";
        string folderLocal = $"{this.linkLocal}FileNodePath";

        foreach (var level in Enum.GetNames(typeof(Level)))
        {
            if (!Directory.Exists($"{folderLocal}/{level}"))
                Directory.CreateDirectory($"{folderLocal}/{level}");

            for (int i = 1; i <= this.maxNodePath; i++)
            {
                string nameFile = level + i;
                StorageReference storageReference = firebaseStorage.GetReferenceFromUrl($"{folderFirebase}{level}%2F{nameFile}");
                storageReference.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
                {
                    if (!task.IsFaulted && !task.IsCanceled && !File.Exists($"{folderLocal}/{level}/{nameFile}"))
                        webClient.DownloadFile(task.Result, $"{folderLocal}/{level}/{nameFile}");
                });
            }
        }
    }
    private void CheckingNodeBuilding(FirebaseStorage firebaseStorage, WebClient webClient)
    {
        string folderFirebase = $"{this.linkFirebase}FileNodeBuilding%2F";
        string folderLocal = $"{this.linkLocal}FileNodeBuilding";

        foreach (var level in Enum.GetNames(typeof(Level)))
        {
            if (!Directory.Exists($"{folderLocal}/{level}"))
                Directory.CreateDirectory($"{folderLocal}/{level}");

            StorageReference storageReference = firebaseStorage.GetReferenceFromUrl($"{folderFirebase}{level}%2F{level}");
            storageReference.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
            {
                if (!task.IsFaulted && !task.IsCanceled && !File.Exists($"{folderLocal}/{level}/{level}"))
                    webClient.DownloadFile(task.Result, $"{folderLocal}/{level}/{level}");
            });
        }
    }
}
