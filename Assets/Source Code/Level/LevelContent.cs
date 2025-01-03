using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelContent : MonoBehaviour
{
    [SerializeField] GameObject btnLevel;
    [SerializeField] GameObject panelUpdating;
    [SerializeField] TextMeshProUGUI textButtonPlayGame;
    [SerializeField] List<GameObject> lsButtonLevel;

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
        int countLevelLocal = 0;

        if (CheckFileForUpdating.IsUpdate() == true)
        {
            this.panelUpdating.gameObject.SetActive(true);
        }

        countLevelLocal = Directory.GetDirectories(FileLocalLink.UserFolderNodeBuilding).Length;
        CreateObjectPooling(countLevelLocal);

        PlayerPrefs.SetString("LEVEL", string.Empty);
        PlayerPrefs.Save();

        if (PlayerPrefs.GetInt("LEVEL_2") == 1)
        {
            List<GameObject> ls = new List<GameObject>();
            foreach (var temp in lsButtonLevel)
            {
                if (PlayerPrefs.GetInt(temp.gameObject.name) == 1)
                {
                    temp.gameObject.SetActive(true);
                    temp.GetComponentInChildren<TextMeshProUGUI>().text = temp.gameObject.name;
                    temp.GetComponentInChildren<TextMeshProUGUI>().text += "\nPassed this level!";
                    if (CheckFileForUpdating.IsNullLocalFolder(temp.gameObject.name))
                    {
                        temp.gameObject.SetActive(false);
                    }
                    ls.Add(temp);
                }
            }
            ls.Last().GetComponentInChildren<TextMeshProUGUI>().text = ls.Last().gameObject.name;
        }
    }
    public void ButtonUpdateNewestVersion()
    {
        SceneManager.LoadScene(SceneNameManager.SceneSplash);
        return;
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
            {
                this._btnLevel.SetActive(true);
                this._btnLevel.GetComponentInChildren<TextMeshProUGUI>().text = $"LEVEL_{i}";
            }

            lsButtonLevel.Add(this._btnLevel);
        }
    }
    public void SetTextButtonPlayGame(string text)
    {
        _levelSelection = LevelSelection.Instance;
        this.textButtonPlayGame.text = text;
        if (_levelSelection.GetButtonStartPlay().activeSelf == false)
            _levelSelection.GetButtonStartPlay().SetActive(true);
    }
}
