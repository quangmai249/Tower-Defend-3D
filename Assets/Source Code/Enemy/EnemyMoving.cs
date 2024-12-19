using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] Vector3[] arrayPoint;
    [SerializeField] Vector3 lastPoint;

    [SerializeField] float randPath = 1f;
    [SerializeField] float timeDurationSlowing;
    [SerializeField] bool isSlowing = false;

    private Tween tween;
    private void Update()
    {
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
    public void Moving()
    {
        this.gameObject.transform.DORestart();
        this.tween = gameObject.transform
            .DOPath(this.ArrayPointEnemyMoving(this.gameObject.transform.position.y)
            , this.gameObject.GetComponent<EnemyManager>().GetEnemyStats().EnemySpeed, PathType.Linear)
            .SetEase(Ease.Linear)
            .SetLookAt(0.001f);
    }
    public void StopMoving()
    {
        this.tween.timeScale = 0f;
    }
    private Vector3[] ArrayPointEnemyMoving(float yPos)
    {
        List<Vector3> ls = new List<Vector3>();
        Vector3 randVec = RandomVector3Path(this.randPath);

        for (int i = 1; i < this.arrayPoint.Length - 1; i++)
            ls.Add(new Vector3(this.arrayPoint[i].x, yPos, this.arrayPoint[i].z) + randVec);

        ls.Add(this.lastPoint);
        return ls.ToArray();
    }
    private Vector3 RandomVector3Path(float rand)
    {
        return new Vector3(Random.Range(-rand, rand), 0, Random.Range(-rand, rand));
    }
}
