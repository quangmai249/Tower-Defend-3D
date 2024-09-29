using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] Level level = Level.LEVEL_1;
    [SerializeField] readonly string pathFileNode = "Assets/SaveFile/FileNodePath";
    [SerializeField] readonly string pathFileNodeBuilding = "Assets/SaveFile/FileNodeBuilding";
    [Header("Node Building")]
    [SerializeField] GameObject nodeBuilding;
    [SerializeField] List<Transform> lsBuilding;
    [Header("Node Path")]
    [SerializeField] GameObject nodePath;
    [SerializeField] List<Transform> lsNodePath;

    private FilePath filePathNode;
    private FilePath filePathBuilding;
    private void Start()
    {
        filePathNode = new FilePath(this.pathFileNode, this.GetLevel());
        filePathBuilding = new FilePath(this.pathFileNodeBuilding, this.GetLevel());

        AddGameObjectToList(nodeBuilding, lsBuilding);
        AddGameObjectToList(nodePath, lsNodePath);

        filePathNode.SetListVector(lsNodePath);
        filePathBuilding.SetListVector(lsBuilding);

        filePathNode.StartSaveToFile();
        filePathBuilding.StartSaveToFile();
    }
    private void AddGameObjectToList(GameObject go, List<Transform> ls)
    {
        foreach (Transform t in go.GetComponentsInChildren<Transform>())
            ls.Add(t);
        ls.RemoveAt(0);
    }
    private string GetLevel()
    {
        return this.level.ToString();
    }
}
