using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
public class PathManager : MonoBehaviour
{
    //[SerializeField] readonly string path = "Assets/Resources/FileNodePath";
    [SerializeField] readonly string path = "F:/unity/Build Game Folders/Tower Defend 3D/Resources/FileNodePath";
    [SerializeField] readonly string levelDesignTag = "Level Design";
    [SerializeField] Vector3[] arrPosNodePath;

    private FilePath filePath;
    private LevelDesign levelDesign;
    private SingletonNodePath singletonNodePath;
    private void Awake()
    {
        levelDesign = GameObject.FindGameObjectWithTag(this.levelDesignTag).GetComponent<LevelDesign>();
        filePath = new FilePath(this.path, levelDesign.GetLevel());
    }
    private void Start()
    {
        if (!File.Exists(filePath.GetPath()))
        {
            Debug.LogError($"File {filePath.GetPath()} NOT EXISTED!");
            return;
        }

        singletonNodePath = SingletonNodePath.Instance;

        this.arrPosNodePath = filePath.ReadFromFile();
        foreach (Vector3 item in arrPosNodePath)
        {
            singletonNodePath.InstantiateNodePathAt(item);
        }
    }
    public Vector3 GetPosSpawnEneny()
    {
        return arrPosNodePath[0];
    }
    public string GetPath()
    {
        return this.path;
    }
}
