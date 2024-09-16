using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] FilePath filePath;
    [SerializeField] List<GameObject> VecPoint = new List<GameObject>();
    [SerializeField] PathType pathType = PathType.Linear;
    [SerializeField] Ease ease = Ease.Linear;
    [SerializeField] float DelayStartFollow = 5f;
    [SerializeField] float DurationFollow = 10f;
    [SerializeField] Vector3 PosDefaultEnemy;
    [SerializeField] Quaternion RotosDefaultEnemy;
    private Tween t;
    IEnumerator Start()
    {
        PosDefaultEnemy = Enemy.transform.position;
        RotosDefaultEnemy = Enemy.transform.rotation;
        yield return new WaitForSeconds(this.DelayStartFollow);
        StartFollow();
    }
    private void Update()
    {
        if (Enemy.transform.position == VecPoint.Last().transform.position)
        {
            Enemy.transform.SetLocalPositionAndRotation(PosDefaultEnemy, RotosDefaultEnemy);
            StartFollow();
        }
    }
    private void StartFollow()
    {
        t = Enemy.transform
            .DOPath(GetArrayVectorPoint(this.VecPoint), this.DurationFollow, this.pathType)
            .SetEase(this.ease).SetOptions(false).SetLookAt(0.001f);
    }
    public Vector3[] GetArrayVectorPoint(List<GameObject> ls_vec)
    {
        Vector3[] ls = new Vector3[this.VecPoint.Count];
        for (int i = 0; i < this.VecPoint.Count; i++)
        {
            ls[i] = this.VecPoint[i].transform.position;
        }
        return ls;
    }
    public List<GameObject> GetListVec()
    {
        return VecPoint;
    }
}
