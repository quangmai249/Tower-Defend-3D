using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
public class PathManager : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    [SerializeField] TextMeshProUGUI textLevel;

    [SerializeField] Transform objectPath;

    [SerializeField] FilePath filePath;

    private List<Transform> ls = new List<Transform>();
    private string path = "Assets/Path Enemy";
    private void Start()
    {
        textLevel.text = levelManager.ToString().Replace('_', ' ');
        filePath = new FilePath(path, levelManager.ToString());

        if (File.Exists(filePath.GetPath()))
            Debug.Log("File existed");
        else
        {
            foreach (Transform go in objectPath.GetComponentInChildren<Transform>())
                ls.Add(go);
            filePath.SetListVector(ls);
            filePath.StartSavePathToFile();
            Debug.Log("Create a new file to Save Path successfully!");
        }
    }
    public string GetFolderPath()
    {
        return this.path;
    }
    public string GetLevelManager()
    {
        return this.levelManager.ToString();
    }
}
