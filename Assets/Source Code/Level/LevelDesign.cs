using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesign : MonoBehaviour
{
    [SerializeField] Level level = Level.LEVEL_1;
    public string GetLevel()
    {
        return this.level.ToString();
    }
}
