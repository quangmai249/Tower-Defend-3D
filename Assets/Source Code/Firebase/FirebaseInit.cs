using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] TextMeshProUGUI textStatusConnectToInternet;

    [Header("Stats")]
    [SerializeField] string apiFirebaseDefault = "https://firebasestorage.googleapis.com/v0/b/tower-defend-3d-unity-84f17.appspot.com/o/";

    [Header("List")]
    [SerializeField] List<string> lsAddr = new List<string>();
    [SerializeField] List<string> lsToken = new List<string>();

    private void Start()
    {
        this.textStatusConnectToInternet.text = string.Empty;
        this.imgTimeLoading.fillAmount = 0;

        if (!Directory.Exists(FileLocalLink.UserRootLocal))
        {
            StartCoroutine(nameof(this.CoroutineResetLevel));
            StartCoroutine(nameof(this.CoroutineDefaultSetting));
        }

        StartCoroutine(nameof(this.CoroutineTimeLoading));
    }
    private void Update()
    {
        if (this.imgTimeLoading.fillAmount == 1f)
        {
            SceneManager.LoadScene(SceneNameManager.SceneCutScene);
            return;
        }
    }
    private IEnumerator CoroutineTimeLoading()
    {
        yield return new WaitForSeconds(2f);

        this.CheckingForUpdating();

        while (this.imgTimeLoading.fillAmount < 1)
        {
            yield return new WaitForSeconds(.5f);
            this.imgTimeLoading.fillAmount += .25f;
            this.textNotify.text = $"Loading... {(this.imgTimeLoading.fillAmount * 100).ToString()}%";
        }
    }
    private void CheckingForUpdating()
    {
        try
        {
            using (WebClient webClient = new WebClient())
            {
                Stream stream = webClient.OpenRead(this.apiFirebaseDefault);
                if (stream.CanRead)
                {
                    this.textStatusConnectToInternet.text = "You're online. We're checking for updating newest maps!";

                    StreamReader sr = new StreamReader(stream);
                    FileData res = JsonConvert.DeserializeObject<FileData>(sr.ReadToEnd());

                    foreach (FileItem item in res.Items)
                    {
                        lsAddr.Add(FileDownload.GetAddress(webClient, this.apiFirebaseDefault + Uri.EscapeDataString(item.Name)));
                        lsToken.Add(FileDownload.GetDownloadToken(webClient, this.apiFirebaseDefault + Uri.EscapeDataString(item.Name)));
                    }
                    this.GetFilesFromFirebaseStorage(webClient);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            this.textStatusConnectToInternet.text = "You're offline. You should connect to the Internet for updating newest maps!";
        }
    }
    private void GetFilesFromFirebaseStorage(WebClient webClient)
    {
        foreach (var item in lsAddr)
        {
            if (!File.Exists(FileLocalLink.UserRootLocal + "/" + item))
            {
                Directory.CreateDirectory(this.SetLinkLocalFile(FileLocalLink.UserRootLocal + "/", item));
                webClient.DownloadFile($"{apiFirebaseDefault}{Uri.EscapeDataString(item)}?alt=media&token={lsToken[lsAddr.IndexOf(item)]}"
                    , this.SetLinkLocalFile(FileLocalLink.UserRootLocal + "/", item) + "/" + this.SetNameLocalFile(item));
            }
        }
    }
    private string SetLinkLocalFile(string linkLocal, string name)
    {
        return (linkLocal + name).Remove((linkLocal + name).LastIndexOf("/") + 1);
    }
    private string SetNameLocalFile(string name)
    {
        return name.Substring(name.LastIndexOf("/") + 1);
    }
    private IEnumerator CoroutineResetLevel()
    {
        yield return new WaitForSeconds(1f);

        PlayerPrefs.SetInt($"LEVEL_1", 1);
        PlayerPrefs.Save();

        for (int i = 2; i <= Enum.GetValues(typeof(Level)).Length; i++)
        {
            PlayerPrefs.SetInt($"LEVEL_{i}", 0);
            PlayerPrefs.Save();
        }
    }
    IEnumerator CoroutineDefaultSetting()
    {
        yield return new WaitForEndOfFrame();
        Screen.SetResolution(1920, 1080, true);
        Setting defaultStats = new Setting(true, 0.5f, 0.5f);
        PlayerPrefs.SetString("Setting Game", JsonUtility.ToJson(defaultStats));
        PlayerPrefs.Save();
    }
}
