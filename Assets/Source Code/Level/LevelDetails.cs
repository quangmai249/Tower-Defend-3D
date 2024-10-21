using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelDetails : MonoBehaviour
{
    [SerializeField] GameObject buttonPlayGame;
    [SerializeField] Image imgLock;
    [SerializeField] bool isUnlock;
    private void Awake()
    {
        foreach (var item in Enum.GetValues(typeof(Level)))
        {
            if (this.gameObject.name.Equals(item.ToString()))
            {
                this.isUnlock = true;
                this.imgLock.gameObject.SetActive(false);
                break;
            }
        }
    }
    private void Start()
    {
        this.buttonPlayGame.SetActive(false);

        if (this.isUnlock == true)
            PlayerPrefs.SetInt(this.gameObject.name, 1);
        else
            PlayerPrefs.SetInt(this.gameObject.name, 0);

        PlayerPrefs.Save();
    }
    public void ButtonSelectLevel()
    {
        if (PlayerPrefs.GetInt(this.gameObject.name) == 0)
            return;

        PlayerPrefs.SetString("LEVEL", this.gameObject.name);
        PlayerPrefs.Save();

        this.buttonPlayGame.SetActive(true);
        return;
    }
}
