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

    [SerializeField] float randPath = 0.5f;
    [SerializeField] float timeDuration = 100f;

    private LevelDesign levelDesign;
    private PathManager pathManager;
    private FilePath filePath;

    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameManager.Instance;

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
            gameManager.SetLives(-1);
            DOTween.Kill(this.gameObject.transform);
            Destroy(this.gameObject);
        }
    }
    private void Moving()
    {
        gameObject.transform
            .DOPath(NewArrayPoint(), timeDuration, PathType.Linear)
            .SetEase(Ease.Linear)
            .SetLookAt(0.001f);
    }
    private Vector3[] NewArrayPoint()
    {
        Vector3[] res = new Vector3[arrayPoint.Length];
        res[0] = arrayPoint[0];
        res[res.Length - 1] = arrayPoint[arrayPoint.Length - 1];
        for (int i = 1; i < arrayPoint.Length - 1; i++)
        {
            res[i] = arrayPoint[i] + RandomVector3Path(randPath);
        }
        return res;
    }
    private Vector3 RandomVector3Path(float rand)
    {
        return new Vector3(Random.Range(-rand, rand), 0, Random.Range(-rand, rand));
    }
}
