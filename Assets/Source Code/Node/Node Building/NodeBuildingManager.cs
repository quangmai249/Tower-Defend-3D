using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class NodeBuildingManager : MonoBehaviour
{
    [SerializeField] Vector3[] arrPosNodeBuilding;

    private FilePath filePath;
    private SingletonBuilding singletonBuilding;
    private LevelDesign levelDesign;
    private void Awake()
    {
        levelDesign = GameObject.FindGameObjectWithTag(GameObjectTagManager.TagLevelDesign).GetComponent<LevelDesign>();
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

        StartCoroutine(nameof(this.CoroutineSpawnNodeBuilding));
    }
    IEnumerator CoroutineSpawnNodeBuilding()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < this.arrPosNodeBuilding.Length; i++)
        {
            yield return new WaitForSeconds(.2f);
            GameObject nodeBuilding = singletonBuilding.InstantiateAt(this.arrPosNodeBuilding[i]);
        }
    }
    private string GetPath()
    {
        return FileLocalLink.UserFolderNodeBuilding + levelDesign.GetLevel();
    }
}
