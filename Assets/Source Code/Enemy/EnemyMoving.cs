using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] readonly string pathManagerTag = "Path Manager";
    [SerializeField] readonly string levelDesignTag = "Level Design";
    [SerializeField] Vector3[] arrayPoint;

    [SerializeField] float timeDuration = 100f;

    private LevelDesign levelDesign;
    private PathManager pathManager;
    private FilePath filePath;
    private void Awake()
    {
        levelDesign = GameObject.FindGameObjectWithTag(levelDesignTag).GetComponent<LevelDesign>();
        pathManager = GameObject.FindGameObjectWithTag(pathManagerTag).GetComponent<PathManager>();
        filePath = new FilePath(pathManager.GetPath(), levelDesign.GetLevel());

        arrayPoint = filePath.ReadFromFile();
    }
    private void Start()
    {
        StartCoroutine(nameof(Moving));
    }
    private void Update()
    {
        if (gameObject.transform.position == arrayPoint[arrayPoint.Length - 1])
        {
            DOTween.Kill(this.gameObject.transform);
            Destroy(this.gameObject);
        }
    }
    private void Moving()
    {
        gameObject.transform
            .DOPath(arrayPoint, timeDuration, PathType.Linear)
            .SetEase(Ease.Linear)
            .SetLookAt(0.001f);
    }
}
