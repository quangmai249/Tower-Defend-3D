using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] GameObject protectThisHome;

    [SerializeField] float timeDuration = 20f;

    [SerializeField] string pathManagerTag = "Path Manager";

    [SerializeField] Vector3[] arrayPoint;

    private PathManager pathManager;
    private FilePath filePath;
    private void Start()
    {
        pathManager = GameObject
            .FindGameObjectWithTag(pathManagerTag)
            .GetComponent<PathManager>();

        CheckPathManager();

        filePath = new FilePath(pathManager.GetFolderPath(), pathManager.GetLevelManager());

        StartCoroutine(nameof(StartMoving));

        protectThisHome.transform.position = arrayPoint[arrayPoint.Length - 1];
    }
    private void Update()
    {
        if (gameObject.transform.position == protectThisHome.transform.position)
        {
            Destroy(gameObject);
        }
    }
    void StartMoving()
    {
        arrayPoint = filePath.ReadPathFromFile();
        Moving();
    }
    private void Moving()
    {
        gameObject.transform
            .DOPath(arrayPoint, timeDuration, PathType.Linear)
            .SetEase(Ease.Linear)
            .SetLookAt(0.001f);
    }
    private void CheckPathManager()
    {
        if (pathManager == null)
        {
            Debug.LogError("Path Manager is null!");
            return;
        }
    }
    public void SetTimeDuration(float t)
    {
        this.timeDuration = t;
    }
}
