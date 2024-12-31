using UnityEngine;

public class LevelDetails : MonoBehaviour
{
    public void ButtonSelectLevel()
    {
        if (PlayerPrefs.GetInt(this.gameObject.name) == 0)
            return;

        PlayerPrefs.SetString("LEVEL", this.gameObject.name);
        PlayerPrefs.Save();

        LevelContent levelContent = LevelContent.Instance;
        levelContent.SetTextButtonPlayGame($"PLAY {this.gameObject.name.Replace("_", " ")}");

        return;
    }
}
