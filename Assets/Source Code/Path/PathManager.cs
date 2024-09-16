using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PathManager : MonoBehaviour
{
    [SerializeField] WaveManager waveManager;
    [SerializeField] FollowPath FollowPath;
    [SerializeField] FilePath filePath;
    [SerializeField] bool isSaveThisPath = true;
    private void Start()
    {
        filePath = new FilePath("Assets/Path Enemy", waveManager.ToString());
        if (isSaveThisPath)
        {
            filePath.SetListVector(FollowPath.GetListVec());
            filePath.StartSavePathToFile();
        }
    }
}
