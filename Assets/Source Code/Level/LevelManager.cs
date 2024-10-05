using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] Level level = Level.LEVEL_1;
    [SerializeField] readonly string pathFileNode = "Assets/Resources/FileNodePath";
    [SerializeField] readonly string pathFileNodeBuilding = "Assets/Resources/FileNodeBuilding";

    private readonly string hidden_path_node = "F:/unity/Build Game Folders/Tower Defend 3D/Resources/FileNodePath";
    private readonly string hidden_path_node_building = "F:/unity/Build Game Folders/Tower Defend 3D/Resources/FileNodeBuilding";

    [Header("Node Building")]
    [SerializeField] GameObject nodeBuilding;
    [SerializeField] List<Transform> lsBuilding;
    [Header("Node Path")]
    [SerializeField] GameObject nodePath;
    [SerializeField] List<Transform> lsNodePath;

    private FilePath filePathNode;
    private FilePath filePathBuilding;
    private FilePath hidden1;
    private FilePath hidden2;
    private void Start()
    {
        AddGameObjectToList(this.nodeBuilding, this.lsBuilding);
        AddGameObjectToList(this.nodePath, this.lsNodePath);

        this.filePathNode = new FilePath(this.pathFileNode, this.GetLevel());
        this.filePathNode.SetListVector(this.lsNodePath);
        this.filePathNode.StartSaveToFile();

        this.filePathBuilding = new FilePath(this.pathFileNodeBuilding, this.GetLevel());
        this.filePathBuilding.SetListVector(this.lsBuilding);
        this.filePathBuilding.StartSaveToFile();

        //hidden
        this.hidden1 = new FilePath(this.hidden_path_node, this.GetLevel());
        this.hidden2 = new FilePath(this.hidden_path_node_building, this.GetLevel());
        this.hidden1.SetListVector(this.lsNodePath);
        this.hidden1.StartSaveToFile();
        this.hidden2.SetListVector(this.lsBuilding);
        this.hidden2.StartSaveToFile();
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
