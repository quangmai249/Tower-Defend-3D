using Firebase.Extensions;
using Firebase.Storage;
using System;
using System.Collections;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FirebaseInit : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Image imgTimeLoading;
    [SerializeField] TextMeshProUGUI textNotify;

    [Header("Stats")]
    [SerializeField] int maxNodePath = 12;
    [SerializeField] string linkLocal = "C:/Tower Defend 3D";
    [SerializeField] string linkFirebase = "https://firebasestorage.googleapis.com/v0/b/tower-defend-3d-unity-84f17.appspot.com/o/";
    private void Awake()
    {
        Screen.fullScreen = true;
        StartCoroutine(nameof(this.CoroutineTimeLoading));
    }
    private void Update()
    {
        if (this.imgTimeLoading.fillAmount == 1f)
        {
            SceneManager.LoadScene("Cut Scene");
            return;
        }
    }
    private IEnumerator CoroutineTimeLoading()
    {
        this.imgTimeLoading.fillAmount = 0f;
        this.textNotify.text = $"We need to check some Recourses in a short time!\n (If this is the first time you start, you should Connect the Internet!)";

        yield return new WaitForEndOfFrame();
        if (this.CheckRecourseFiles() == true)
        {
            while (this.imgTimeLoading.fillAmount < 1)
            {
                yield return new WaitForSeconds(1f);
                this.imgTimeLoading.fillAmount += 0.25f;
            }
        }
        else
        {
            WebClient webClient = new WebClient();
            FirebaseStorage firebaseStorage = FirebaseStorage.DefaultInstance;
            this.CheckingNodePath(firebaseStorage, webClient);
            this.CheckingNodeBuilding(firebaseStorage, webClient);

            StartCoroutine(nameof(this.CoroutineResetLevel));
            StartCoroutine(nameof(this.CoroutineTimeLoading));
            StartCoroutine(nameof(this.CoroutineDefaultSetting));
        }
    }
    private bool CheckRecourseFiles()
    {
        bool res = false;
        foreach (var level in Enum.GetNames(typeof(Level)))
        {
            if (!Directory.Exists($"{this.linkLocal}/FileNodeBuilding/{level}") || !Directory.Exists($"{this.linkLocal}/FileNodePath/{level}"))
                return false;
            else res = true;
        }
        return res;
    }
    private IEnumerator CoroutineResetLevel()
    {
        yield return new WaitForEndOfFrame();

        PlayerPrefs.SetInt($"LEVEL_1", 1);
        PlayerPrefs.Save();

        for (int i = 2; i <= Enum.GetValues(typeof(Level)).Length; i++)
        {
            PlayerPrefs.SetInt($"LEVEL_{i}", 0);
            PlayerPrefs.Save();
        }
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
    IEnumerator CoroutineDefaultSetting()
    {
        yield return new WaitForEndOfFrame();
        Setting defaultStats = new Setting(true, 0.5f, 0.5f);
        PlayerPrefs.SetString("Setting Game", JsonUtility.ToJson(defaultStats));
        PlayerPrefs.Save();
    }
}
