using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;

public class LevelContent : MonoBehaviour
{
    [SerializeField] GameObject btnLevel;
    [SerializeField] GameObject btnUpdateNewestLevel;
    [SerializeField] TextMeshProUGUI textButtonPlayGame;
    [SerializeField] List<GameObject> lsButtonLevel;
    [SerializeField] int countLevelLocal;

    private GameObject _btnLevel;
    private LevelSelection _levelSelection;

    public static LevelContent Instance;
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
        try
        {
            using (WebClient webClient = new WebClient())
            {
                Stream stream = webClient.OpenRead("https://firebasestorage.googleapis.com/v0/b/tower-defend-3d-unity-84f17.appspot.com/o/");
                if (stream.CanRead)
                {
                    StreamReader streamReader = new StreamReader(stream);
                    FileData fileData = JsonConvert.DeserializeObject<FileData>(streamReader.ReadToEnd());

                    foreach (var item in fileData.Items)
                    {
                        if (item.Name.Contains("FileNodeBuilding") && !File.Exists("C:/Tower Defend 3D/" + item.Name))
                        {
                            this.btnUpdateNewestLevel.gameObject.SetActive(true);
                        }
                    }
                }
            };
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message.ToString());
            this.countLevelLocal = Directory.GetDirectories("C:/Tower Defend 3D/FileNodeBuilding/").Length;
            Debug.Log(this.countLevelLocal);
            CreateObjectPooling(this.countLevelLocal);
        }

        PlayerPrefs.SetString("LEVEL", string.Empty);
        PlayerPrefs.Save();

        _levelSelection = LevelSelection.Instance;
        this.textButtonPlayGame.text = "NONE";

        foreach (var level in Enum.GetNames(typeof(Level)))
        {
            foreach (var temp in lsButtonLevel)
            {
                if (level.Equals(temp.gameObject.name.ToString()) && PlayerPrefs.GetInt(level) == 1)
                {
                    temp.gameObject.SetActive(true);
                    temp.GetComponentInChildren<TextMeshProUGUI>().text = temp.gameObject.name;
                    break;
                }
            }
        }
    }
    private void CreateObjectPooling(int defaultNumPooling)
    {
        for (int i = 1; i <= defaultNumPooling; i++)
        {
            this._btnLevel = Instantiate(this.btnLevel);
            this._btnLevel.gameObject.name = $"LEVEL_{i}";
            this._btnLevel.gameObject.transform.SetParent(this.gameObject.transform);
            this._btnLevel.SetActive(false);

            if (PlayerPrefs.GetInt(this._btnLevel.gameObject.name) == 1)
                this._btnLevel.SetActive(true);

            lsButtonLevel.Add(this._btnLevel);
        }
    }
    public void SetTextButtonPlayGame(string text)
    {
        this.textButtonPlayGame.text = text;
        if (_levelSelection.GetButtonStartPlay().activeSelf == false)
            _levelSelection.GetButtonStartPlay().SetActive(true);
    }
}
