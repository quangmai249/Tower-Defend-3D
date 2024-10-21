using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] Vector3[] arrayPoint;
    [SerializeField] Vector3 lastPoint;

    [SerializeField] float randPath = 1f;
    [SerializeField] float timeDuration = 30f;
    [SerializeField] float timeDurationSlowing;
    [SerializeField] bool isSlowing = false;

    private GameManager gameManager;
    private GameStats gameStats;
    private Tween tween;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        gameStats = gameManager.GetGameStats();
    }
    private void Start()
    {
        StartCoroutine(nameof(Moving));
    }
    private void Update()
    {
        if (gameObject.transform.position == this.lastPoint)
        {
            gameStats.Lives -= 1;
            DOTween.Kill(this.gameObject.transform);
            Destroy(this.gameObject);
        }
        if (isSlowing == true)
        {
            this.tween.timeScale = timeDurationSlowing;
            isSlowing = false;
            return;
        }
        else
        {
            this.tween.timeScale = 1;
            return;
        }
    }
    public void SetArrayPoint(FilePath f)
    {
        this.arrayPoint = f.ReadFromFile();
        this.lastPoint = this.arrayPoint[this.arrayPoint.Length - 1];
    }
    public void SetIsSlowing(bool b, float timeDurationSlowing)
    {
        this.isSlowing = b;
        this.timeDurationSlowing = timeDurationSlowing;
    }
    private void Moving()
    {
        this.tween =
        gameObject.transform
            .DOPath(ArrayPointEnemyMoving(), timeDuration, PathType.Linear)
            .SetEase(Ease.Linear)
            .SetLookAt(0.001f);
    }
    private Vector3[] ArrayPointEnemyMoving()
    {
        Vector3[] res = new Vector3[this.arrayPoint.Length];
        res[0] = this.arrayPoint[1];
        res[this.arrayPoint.Length - 1] = this.arrayPoint[this.arrayPoint.Length - 1];
        for (int i = 1; i < this.arrayPoint.Length - 1; i++)
            res[i] = this.arrayPoint[i] + RandomVector3Path(this.randPath);
        return res;
    }
    private Vector3 RandomVector3Path(float rand)
    {
        return new Vector3(Random.Range(-rand, rand), 0, Random.Range(-rand, rand));
    }
}
