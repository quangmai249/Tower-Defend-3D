using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditLevelManager : MonoBehaviour
{
    [Header("Confirm")]
    [SerializeField] GameObject panelConfirmDel;
    [SerializeField] TextMeshProUGUI textNotifyDel;
    [SerializeField] TextMeshProUGUI textConfirmDel;
    [SerializeField] string level;

    [Header("Node")]
    [SerializeField] GameObject panelConfirmSaveToLocal;

    [Header("Path")]
    [SerializeField] TextMeshProUGUI textNotifyCommon;
    [SerializeField] TMPro.TMP_Dropdown tMP_Dropdown;

    private TMPro.TMP_Dropdown.OptionData optionData;
    private SingletonBuilding singletonBuilding;
    private SingletonNodePath singletonNodePath;
    void Start()
    {
        singletonBuilding = SingletonBuilding.Instance;
        singletonNodePath = SingletonNodePath.Instance;

        this.textNotifyDel.text = string.Empty;
        this.textNotifyCommon.text = string.Empty;

        this.tMP_Dropdown.options.Clear();
        this.panelConfirmDel.gameObject.SetActive(false);
        this.panelConfirmSaveToLocal.gameObject.SetActive(false);

        foreach (var level in Enum.GetNames(typeof(Level)))
        {
            this.optionData = new TMPro.TMP_Dropdown.OptionData();
            this.optionData.text = level;
            this.tMP_Dropdown.options.Add(this.optionData);
        }
    }
    private void Update()
    {
        this.level = this.tMP_Dropdown.captionText.text.ToString();
        this.textConfirmDel.text = $"Are you sure delete {this.level}?";
    }
    public void ButtonLoadNode()
    {
        this.textNotifyCommon.text = this.level + " is running!";
        this.panelConfirmDel.gameObject.SetActive(false);
        this.panelConfirmSaveToLocal.gameObject.SetActive(false);
        this.StartReadNodeBuilding();
        this.StartReadNodePath();
        return;
    }
    public void ButtonHome()
    {
        SceneManager.LoadScene(SceneNameManager.SceneMenuGame);
        return;
    }
    public void ButtonDeleteLevel()
    {
        this.textNotifyDel.text = string.Empty;
        this.panelConfirmDel.gameObject.SetActive(true);
        this.panelConfirmSaveToLocal.gameObject.SetActive(false);
        return;
    }
    public void ButtonCancel()
    {
        this.textNotifyDel.text = string.Empty;
        this.panelConfirmDel.gameObject.SetActive(false);
        this.panelConfirmSaveToLocal.gameObject.SetActive(false);
        return;
    }
    public void ButtonConfirmDelete()
    {
        string path_node_building = FileLocalLink.DesignerFolderNodeBuilding + this.level;
        string path_node_path = FileLocalLink.DesignerFolderNodePath + this.level;

        try
        {
            DirectoryInfo directoryInfoNodeBuilding = new DirectoryInfo(path_node_building);
            DirectoryInfo directoryInfoNodePath = new DirectoryInfo(path_node_path);

            foreach (var item in directoryInfoNodeBuilding.GetFiles())
                item.Delete();
            foreach (var item in directoryInfoNodePath.GetFiles())
                item.Delete();

            directoryInfoNodeBuilding.Delete();
            directoryInfoNodePath.Delete();

            this.textNotifyDel.text
                = $"Delete {this.level} successfully!";
            return;
        }
        catch (Exception ex)
        {
            this.textNotifyDel.text = ex.Message.ToString();
            return;
        }
    }
    private void StartReadNodeBuilding()
    {
        string path = FileLocalLink.DesignerFolderNodeBuilding + this.level;
        try
        {
            FilePath f = new FilePath(path, this.level);

            foreach (var item in GameObject.FindGameObjectsWithTag(GameObjectTagManager.TagNodeBuilding))
                item.gameObject.SetActive(false);
            foreach (var item in GameObject.FindGameObjectsWithTag(GameObjectTagManager.TagNodePath))
                Destroy(item.gameObject);

            Vector3[] vec = f.ReadFromFile();
            foreach (var item in vec)
                singletonBuilding.InstantiateAt(item);
        }
        catch (Exception ex)
        {
            this.textNotifyCommon.text = ex.Message.ToString();
            return;
        }
    }
    private void StartReadNodePath()
    {
        string path = FileLocalLink.DesignerFolderNodePath + this.level;

        try
        {
            for (int i = 0; i < Directory.GetFiles(path).Length; i++)
            {
                FilePath f = new FilePath(path, this.level + (i + 1));
                Vector3[] vec = f.ReadFromFile();

                GameObject g = singletonNodePath.InstantiateNodePathAt(vec[0]);
                g.gameObject.transform.SetParent(this.gameObject.transform);

                for (int j = 1; j < vec.Length; j++)
                    singletonNodePath.InstantiateNodePathAt(vec[j]).gameObject.transform.SetParent(g.gameObject.transform);
            }
        }
        catch (Exception ex)
        {
            this.textNotifyCommon.text = ex.Message.ToString();
            return;
        }
    }
}
