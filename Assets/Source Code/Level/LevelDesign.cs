using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDesign : MonoBehaviour
{
    [SerializeField] Level level = Level.LEVEL_1;
    [SerializeField] TextMeshProUGUI textLevel;
    [SerializeField] bool checking = false;
    private void Start()
    {
        if (this.checking == true)
            this.textLevel.text = level.ToString().Replace('_', ' ');
        else
            this.textLevel.text = PlayerPrefs.GetString("LEVEL").Replace('_', ' ');
    }
    public string GetLevel()
    {
        if (this.checking == true)
            return this.level.ToString();

        return PlayerPrefs.GetString("LEVEL");
    }
}
