using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NodeBuildingManager : MonoBehaviour
{
    [SerializeField] readonly string path = "Assets/SaveFile/FileNodeBuilding";
    [SerializeField] readonly string levelDesignTag = "Level Design";
    [SerializeField] Vector3[] arrPosNodeBuilding;

    private FilePath filePath;
    private SingletonBuilding singletonBuilding;
    private LevelDesign levelDesign;
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

        singletonBuilding = SingletonBuilding.Instance;

        this.arrPosNodeBuilding = filePath.ReadFromFile();
        foreach (Vector3 item in arrPosNodeBuilding)
        {
            singletonBuilding.InstantiateAt(item);
        }
    }
}
