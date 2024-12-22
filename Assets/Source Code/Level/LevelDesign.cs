using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDesign : MonoBehaviour
{
    [SerializeField] Level level = Level.LEVEL_1;
    [SerializeField] TextMeshProUGUI textLevel;
    [SerializeField] GameObject pannelRebootsGame;

    [SerializeField] bool checking = false;

    public static LevelDesign Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"{this.gameObject.name} is NOT NULL!");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        this.pannelRebootsGame.gameObject.SetActive(false);
        if (this.checking == true)
            this.textLevel.text = level.ToString().Replace('_', ' ');
        else
            this.textLevel.text = PlayerPrefs.GetString("LEVEL").Replace('_', ' ');
        this.CheckFolderRescourses();
    }
    private void CheckFolderRescourses()
    {
        if (!Directory.Exists(FileLocalLink.UserFolderNodeBuilding + this.level)
            || !Directory.Exists(FileLocalLink.UserFolderNodePath + this.level)
            || Directory.GetFiles(FileLocalLink.UserFolderNodeBuilding + this.level).Length == 0
            || Directory.GetFiles(FileLocalLink.UserFolderNodePath + this.level).Length == 0)
        {
            this.pannelRebootsGame.gameObject.SetActive(true);
        }
    }
    public string GetLevel()
    {
        if (this.checking == true)
            return this.level.ToString();
        return PlayerPrefs.GetString("LEVEL");
    }
    public int GetLevelTypeInt()
    {
        if (this.checking == true)
            return Int32.Parse(this.level.ToString().Remove(0, 6));
        return Int32.Parse(PlayerPrefs.GetString("LEVEL").Remove(0, 6));
    }
    public void ButtonRebootsGame()
    {
        if (Directory.Exists(FileLocalLink.UserRootLocal))
        {
            if (Directory.Exists(FileLocalLink.UserFolderNodeBuilding + this.level))
            {
                foreach (var file in Directory.GetFiles(FileLocalLink.UserFolderNodeBuilding + this.level))
                {
                    Directory.Delete(file);
                }
            }

            if (Directory.Exists(FileLocalLink.UserFolderNodePath + this.level))
            {
                foreach (var file in Directory.GetFiles(FileLocalLink.UserFolderNodePath + this.level))
                {
                    File.Delete(file);
                }
            }
        }
        SceneManager.LoadScene("Splash Scene");
    }
}
