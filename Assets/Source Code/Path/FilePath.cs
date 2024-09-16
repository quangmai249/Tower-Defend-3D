using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FilePath
{
    private string name;
    private string path;
    private List<GameObject> lsPos;
    public FilePath(string path, string name)
    {
        this.name = name;
        this.path = path;
    }
    public void SetListVector(List<GameObject> position)
    {
        this.lsPos = position;
    }
    public void StartSavePathToFile()
    {
        using (StreamWriter streamWriter = new StreamWriter($"{this.path}/{this.name}", append: false))
        {
            foreach (var item in lsPos)
                streamWriter.WriteLine(JsonUtility.ToJson(item.transform.position));
            streamWriter.Close();
        }
    }
    public List<Vector3> ReadPathFromFile()
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
        return res;
    }
}
