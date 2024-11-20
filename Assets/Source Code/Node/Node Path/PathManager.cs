using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
public class PathManager : MonoBehaviour
{
    [SerializeField] string default_path = "C:/Tower Defend 3D/";

    private List<FilePath> lsFilePath = new List<FilePath>();
    private LevelDesign levelDesign;

    private int numberPath = 0;

    private readonly string path = "FileNodePath/";
    private readonly string levelDesignTag = "Level Design";
    private void Awake()
    {
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
        return this.default_path + this.path + levelDesign.GetLevel();
    }
    private void SetListFilePath(List<FilePath> ls)
    {
        this.numberPath = Directory.GetFiles(this.GetPath(), "*", SearchOption.AllDirectories).Length;
        for (int i = 0; i < this.numberPath; i++)
        {
            FilePath temp = new FilePath(this.GetPath(), levelDesign.GetLevel() + (i + 1));
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
