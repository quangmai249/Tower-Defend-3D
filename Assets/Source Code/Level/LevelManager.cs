using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] TextMeshProUGUI textNotify;
    [SerializeField] TMPro.TMP_Dropdown tMP_Dropdown;
    [SerializeField] string level;

    [Header("Node Building")]
    [SerializeField] GameObject nodeBuilding;
    [SerializeField] List<Transform> lsBuilding;

    [Header("Node Path")]
    [SerializeField] GameObject nodePath;
    [SerializeField] List<Transform> lsNodePath;

    private readonly string hidden_path_node = "F:/unity/Build Game Folders/Tower Defend 3D/Resources/FileNodePath/";
    private readonly string hidden_path_node_building = "F:/unity/Build Game Folders/Tower Defend 3D/Resources/FileNodeBuilding/";

    private FilePath hidden_file_node_path;
    private FilePath hidden_file_node_building;
    private void Start()
    {
        this.textNotify.text = string.Empty;
        this.tMP_Dropdown.ClearOptions();
        this.tMP_Dropdown.captionText.text = Level.LEVEL_1.ToString();

        foreach (var item in Enum.GetValues(typeof(Level)))
        {
            TMPro.TMP_Dropdown.OptionData temp = new TMPro.TMP_Dropdown.OptionData();
            temp.text = item.ToString();
            this.tMP_Dropdown.options.Add(temp);
        }
    }
    public void GetValueLevelDesign()
    {
        try
        {
            this.level = this.tMP_Dropdown.options[this.tMP_Dropdown.value].text;
            StartSaveListNodePathToFile();
            StartSaveListNodeBuildingToFile();
            this.textNotify.text = $"Saved {this.level} successfully!";
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            this.textNotify.text = $"Saved {this.level} unsuccessfully!";
        }
    }
    private void StartSaveListNodeBuildingToFile()
    {
        this.lsBuilding = ListGameObjectNodeBuilding(this.nodeBuilding);
        this.lsBuilding.RemoveAt(0);

        this.hidden_file_node_building
            = new FilePath(this.hidden_path_node_building + this.level.ToString(), this.level.ToString());
        this.hidden_file_node_building.SetListVector(this.lsBuilding);
        this.hidden_file_node_building.StartSaveToFile();
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
                = new FilePath(this.hidden_path_node + this.level.ToString(), this.level.ToString() + count);
            this.hidden_file_node_path.SetListVector(this.lsNodePath);
            this.hidden_file_node_path.StartSaveToFile();

            count++;
            this.lsNodePath.Clear();
        }
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
