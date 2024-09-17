using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FilePath
{
    private string name;
    private string path;
    private List<Transform> lsPos;
    public FilePath(string path, string name)
    {
        this.name = name;
        this.path = path;
    }
    public string GetPath()
    {
        return $"{this.path}/{this.name}";
    }
    public void SetListVector(List<Transform> position)
    {
        this.lsPos = position;
    }
    public void StartSavePathToFile()
    {
        using (StreamWriter streamWriter = new StreamWriter($"{this.path}/{this.name}", append: false))
        {
            foreach (var item in lsPos)
                streamWriter.WriteLine(JsonUtility.ToJson(item.position));
            streamWriter.Close();
        }
    }
    public Vector3[] ReadPathFromFile()
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
}
