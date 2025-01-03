using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] GameObject panelConfirm;
    [SerializeField] TextMeshProUGUI textConfirm;
    [SerializeField] TextMeshProUGUI textNotify;
    [SerializeField] TMPro.TMP_Dropdown tMP_Dropdown;
    [SerializeField] string level;

    [Header("Node Building")]
    [SerializeField] GameObject nodeBuilding;
    [SerializeField] List<Transform> lsBuilding;

    [Header("Node Path")]
    [SerializeField] GameObject nodePath;
    [SerializeField] List<Transform> lsNodePath;

    [Header("Link Default")]
    [SerializeField] GameObject panelConfirmDelete;

    private FilePath hidden_file_node_path;
    private FilePath hidden_file_node_building;
    private void Start()
    {
        this.textConfirm.text = string.Empty;
        this.textNotify.text = string.Empty;

        this.panelConfirm.gameObject.SetActive(false);
        this.panelConfirmDelete.gameObject.SetActive(false);

        this.tMP_Dropdown.ClearOptions();
        this.tMP_Dropdown.captionText.text = Level.LEVEL_1.ToString();
    }
    private void Update()
    {
        this.level = this.tMP_Dropdown.captionText.text.ToString();
        this.textConfirm.text = $"Are you sure create level {this.level}?";
    }
    public void ButtonMenuGame()
    {
        SceneManager.LoadScene(SceneNameManager.SceneSplash);
        return;
    }
    public void ButtonSaveOffline()
    {
        this.textNotify.text = string.Empty;
        this.panelConfirm.gameObject.SetActive(true);
        this.panelConfirmDelete.gameObject.SetActive(false);
        return;
    }
    public void ButtonCancel()
    {
        this.textNotify.text = string.Empty;
        this.panelConfirm.SetActive(false);
        this.panelConfirmDelete.gameObject.SetActive(false);
        return;
    }
    public void ButtonConfirmSaveOffline()
    {
        StartSaveListNodePathToFile();
        StartSaveListNodeBuildingToFile();
        return;
    }
    public void ButtonResetLevelGame()
    {
        PlayerPrefs.SetInt($"LEVEL_1", 1);
        PlayerPrefs.Save();

        for (int i = 2; i <= Enum.GetValues(typeof(Level)).Length; i++)
        {
            PlayerPrefs.SetInt($"LEVEL_{i}", 0);
            PlayerPrefs.Save();
        }

        Setting setting = new Setting(true, 0.5f, 0.5f);
        PlayerPrefs.SetString("Setting Game", JsonUtility.ToJson(setting));
        PlayerPrefs.Save();

        Debug.Log(PlayerPrefs.GetString("Setting Game"));
        Debug.Log("Reset level done!");
    }
    private void StartSaveListNodeBuildingToFile()
    {
        this.lsBuilding = ListGameObjectNodeBuilding(this.nodeBuilding);
        this.lsBuilding.RemoveAt(0);

        this.hidden_file_node_building
            = new FilePath(FileLocalLink.DesignerFolderNodeBuilding + this.level, this.level);
        this.hidden_file_node_building.SetListVector(this.lsBuilding);
        this.hidden_file_node_building.StartSaveToFile();
        this.textNotify.text = this.hidden_file_node_building.GetNotify();

        return;
    }
    private void StartSaveListNodePathToFile()
    {
        int count = 1;
        Transform[] temp = null;
        foreach (var item in ListGameObjectNodePath(this.nodePath))
        {
            temp = item.GetComponentsInChildren<Transform>();
            for (int i = 1; i < temp.Length; i++)
            {
                this.lsNodePath.Add(temp[i]);
            }

            this.hidden_file_node_path
                = new FilePath(FileLocalLink.DesignerFolderNodePath + this.level, this.level + count);

            this.hidden_file_node_path.SetListVector(this.lsNodePath);
            this.hidden_file_node_path.StartSaveToFile();
            this.textNotify.text = this.hidden_file_node_path.GetNotify();

            count++;
            temp = null;
            this.lsNodePath.Clear();
        }
        return;
    }
    private List<Transform> ListGameObjectNodeBuilding(GameObject go)
    {
        List<Transform> ls = new List<Transform>();
        foreach (var item in go.GetComponentsInChildren<Transform>())
        {
            ls.Add(item);
        }
        return ls;
    }
    private List<Transform> ListGameObjectNodePath(GameObject go)
    {
        List<Transform> lsChild = new List<Transform>();

        foreach (var item in go.GetComponentsInChildren<Transform>())
        {
            if (item.parent == go.transform)
                lsChild.Add(item);
        }

        return lsChild;
    }
}
