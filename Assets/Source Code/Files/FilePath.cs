using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FilePath
{
    private string name;
    private string path;
    private List<Transform> lsPos;
    private StreamWriter streamWriter = null;
    public FilePath(string path, string name)
    {
        this.name = name;
        this.path = path;
    }
    public void SetListVector(List<Transform> position)
    {
        this.lsPos = position;
    }
    public void StartSaveToFile()
    {
        CheckPathExisted();
        if (this.lsPos.Count > 0)
        {
            try
            {
                SaveData(streamWriter, this.lsPos);
                Debug.Log($"Saved file {this.path} SUCCESSFULLY!");
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }
        else
        {
            Debug.LogError("List transform is NULL!");
            return;
        }
    }
    public Vector3[] ReadFromFile()
    {
        List<Vector3> res = new List<Vector3>();
        string read_line = string.Empty;
        using (StreamReader streamReader = new StreamReader($"{this.path}/{this.name}"))
        {
            while ((read_line = streamReader.ReadLine()) != null)
            {
                Vector3 vec = JsonUtility.FromJson<Vector3>(read_line);
                res.Add(vec);
            }
            streamReader.Close();
        }
        return res.ToArray();
    }
    private void CheckPathExisted()
    {
        if (File.Exists(this.GetPath()))
        {
            Debug.LogError($"File {this.GetPath()} EXISTED!");
            return;
        }
        else
        {
            Directory.CreateDirectory(this.path);
            SaveData(streamWriter, this.lsPos);
            Debug.Log($"Create new folder {this.path} SUCCESSFULLY!");
        }
    }
    public string GetPath()
    {
        return $"{this.path}/{this.name}";
    }
    private void SaveData(StreamWriter s, List<Transform> ls)
    {
        using (s = new StreamWriter(this.GetPath(), append: false))
        {
            foreach (var item in ls)
                s.WriteLine(JsonUtility.ToJson(item.position));
            s.Close();
        }
    }
}
