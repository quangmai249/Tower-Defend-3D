using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
public class PathManager : MonoBehaviour
{
    private List<FilePath> lsFilePath = new List<FilePath>();
    private LevelDesign levelDesign;
    private SingletonNodePath singletonNodePath;

    private int numberPath = 0;

    private readonly string path = "F:/unity/Build Game Folders/Tower Defend 3D/Resources/FileNodePath/";
    private readonly string levelDesignTag = "Level Design";
    private void Awake()
    {
        singletonNodePath = SingletonNodePath.Instance;
        levelDesign = GameObject.FindGameObjectWithTag(this.levelDesignTag).GetComponent<LevelDesign>();
    }
    private void Start()
    {
        SetListFilePath(this.lsFilePath);
        CheckListFilePath(this.lsFilePath);
    }
    public List<FilePath> GetListFileNodePath()
    {
        return this.lsFilePath;
    }
    public string GetPath()
    {
        return this.path + levelDesign.GetLevel();
    }
    private void SetListFilePath(List<FilePath> ls)
    {
        this.numberPath = Directory.GetFiles(this.path + levelDesign.GetLevel(), "*", SearchOption.AllDirectories).Length;
        for (int i = 0; i < this.numberPath; i++)
        {
            FilePath temp = new FilePath(this.path + levelDesign.GetLevel(), levelDesign.GetLevel() + (i + 1));
            ls.Add(temp);
        }
    }
    private void CheckListFilePath(List<FilePath> ls)
    {
        foreach (var item in ls)
        {
            if (!File.Exists(item.GetPath()))
            {
                Debug.LogError($"File {item.GetPath()} NOT EXISTED!");
                return;
            }
        }
    }
}
