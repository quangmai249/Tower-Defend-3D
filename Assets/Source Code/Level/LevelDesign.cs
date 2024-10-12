using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesign : MonoBehaviour
{
    public string GetLevel()
    {
        return PlayerPrefs.GetString("LEVEL");
    }
}
