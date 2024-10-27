using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditLevelManager : MonoBehaviour
{
    [Header("Confirm")]
    [SerializeField] GameObject btnEdit;
    [SerializeField] GameObject panelConfirm;
    [SerializeField] GameObject btnConfirm;
    [SerializeField] GameObject btnCancel;
    [SerializeField] TextMeshProUGUI textConfirm;
    [SerializeField] TextMeshProUGUI textNotify;

    [Header("Node")]
    [SerializeField] GameObject nodeBuilding;
    [SerializeField] GameObject nodePath;
    [SerializeField] List<GameObject> lsNode;

    [Header("Path")]
    [SerializeField] TMPro.TMP_Dropdown tMP_Dropdown;
    [SerializeField] string default_path = "F:/unity/Build Game Folders/Tower Defend 3D/Resources/";

    private readonly string pathNodeBuilding = "FileNodeBuilding/";
    private readonly string pathNodePath = "FileNodePath/";

    private TMPro.TMP_Dropdown.OptionData optionData;
    private SingletonBuilding singletonBuilding;
    private SingletonNodePath singletonNodePath;
    void Start()
    {
        singletonBuilding = SingletonBuilding.Instance;
        singletonNodePath = SingletonNodePath.Instance;

        this.default_path = "F:/unity/Build Game Folders/Tower Defend 3D/Resources/";
        this.tMP_Dropdown.options.Clear();

        this.textNotify.text = string.Empty;
        this.panelConfirm.gameObject.SetActive(false);
        this.btnEdit.gameObject.SetActive(false);

        foreach (var item in Enum.GetValues(typeof(Level)))
        {
            this.optionData = new TMPro.TMP_Dropdown.OptionData();
            this.optionData.text = item.ToString();
            this.tMP_Dropdown.options.Add(this.optionData);
        }
    }
    public void ButtonLoadNode()
    {
        this.textNotify.text = string.Empty;
        this.btnEdit.gameObject.SetActive(true);
        StartReadNodeBuilding();
        return;
    }
    public void ButtonHome()
    {
        SceneManager.LoadScene("Menu Game");
        return;
    }
    public void ButtonDeleteLevel()
    {
        this.panelConfirm.gameObject.SetActive(true);
        this.textConfirm.text = $"Are you sure delete {this.tMP_Dropdown.options[this.tMP_Dropdown.value].text.ToString()}?";
        return;
    }
    public void ButtonCancel()
    {
        this.panelConfirm.gameObject.SetActive(false);
        this.btnEdit.gameObject.SetActive(false);
        return;
    }
    public void ButtonConfirmDelete()
    {
        string path_node_building = this.default_path + this.pathNodeBuilding + this.tMP_Dropdown.options[this.tMP_Dropdown.value].text.ToString();
        string path_node_path = this.default_path + this.pathNodePath + this.tMP_Dropdown.options[this.tMP_Dropdown.value].text.ToString();

        try
        {
            DirectoryInfo directoryInfoNodeBuilding = new DirectoryInfo(path_node_building);
            DirectoryInfo directoryInfoNodePath = new DirectoryInfo(path_node_path);

            foreach (var item in directoryInfoNodeBuilding.GetFiles())
                item.Delete();
            foreach (var item in directoryInfoNodePath.GetFiles())
                item.Delete();

            this.textConfirm.text
                = $"Delete {this.tMP_Dropdown.options[this.tMP_Dropdown.value].text.ToString()} successfully!";
            return;
        }
        catch (Exception ex)
        {
            this.textNotify.text = ex.Message.ToString();
            return;
        }
    }
    public void ButtonDesignLevel()
    {
        SceneManager.LoadScene("Scene Design Level");
        return;
    }
    private void StartReadNodeBuilding()
    {
        string path = this.default_path + this.pathNodeBuilding + this.tMP_Dropdown.options[this.tMP_Dropdown.value].text.ToString();
        try
        {
            FilePath f = new FilePath(path, this.tMP_Dropdown.options[this.tMP_Dropdown.value].text.ToString());

            foreach (var item in GameObject.FindGameObjectsWithTag("Node Building"))
                Destroy(item.gameObject);

            foreach (var item in GameObject.FindGameObjectsWithTag("Node Path"))
                Destroy(item.gameObject);

            Vector3[] vec = f.ReadFromFile();
            foreach (var item in vec)
                singletonBuilding.InstantiateAt(item).gameObject.transform.SetParent(this.nodeBuilding.transform);

            StartReadNodePath();
        }
        catch (Exception ex)
        {
            this.btnEdit.gameObject.SetActive(false);
            this.textNotify.text = ex.Message.ToString();
            return;
        }
    }
    private void StartReadNodePath()
    {
        string path = this.default_path + this.pathNodePath + this.tMP_Dropdown.options[this.tMP_Dropdown.value].text.ToString();

        try
        {
            for (int i = 0; i < Directory.GetFiles(path).Length; i++)
            {
                FilePath f = new FilePath(path, this.tMP_Dropdown.options[this.tMP_Dropdown.value].text.ToString() + (i + 1));

                Vector3[] vec = f.ReadFromFile();

                GameObject g = singletonNodePath.InstantiateNodePathAt(vec[0]);
                g.gameObject.transform.SetParent(this.nodePath.transform);

                for (int j = 1; j < vec.Length; j++)
                    singletonNodePath.InstantiateNodePathAt(vec[j]).transform.SetParent(g.gameObject.transform);
            }
            this.textNotify.text = this.tMP_Dropdown.options[this.tMP_Dropdown.value].text.ToString();
        }
        catch (Exception ex)
        {
            this.btnEdit.gameObject.SetActive(false);
            this.textNotify.text = ex.Message.ToString();
            return;
        }
    }
}
