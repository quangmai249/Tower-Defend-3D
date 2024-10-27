using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class NodeBuildingManager : MonoBehaviour
{
    [SerializeField] Vector3[] arrPosNodeBuilding;
    [SerializeField] string default_path = "F:/unity/Build Game Folders/Tower Defend 3D/Resources/";

    private FilePath filePath;
    private SingletonBuilding singletonBuilding;
    private LevelDesign levelDesign;

    private readonly string path = "FileNodeBuilding/";
    private readonly string levelDesignTag = "Level Design";
    private void Awake()
    {
        levelDesign = GameObject.FindGameObjectWithTag(this.levelDesignTag).GetComponent<LevelDesign>();
        filePath = new FilePath(this.GetPath(), levelDesign.GetLevel());
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

        for (int i = 0; i < this.arrPosNodeBuilding.Length; i++)
            singletonBuilding.InstantiateAt(this.arrPosNodeBuilding[i]);
    }
    private string GetPath()
    {
        return this.default_path + this.path + levelDesign.GetLevel();
    }
}
