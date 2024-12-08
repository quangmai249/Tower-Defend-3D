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
    [SerializeField] string linkLocal = "C:/Tower Defend 3D";
    [SerializeField] string linkLocalFileNodeBuilding = "C:/Tower Defend 3D/FileNodeBuilding";
    [SerializeField] string linkLocalFileNodePath = "C:/Tower Defend 3D/FileNodePath";

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
        if (!Directory.Exists($"{this.linkLocalFileNodeBuilding}/{this.level}")
            || !Directory.Exists($"{this.linkLocalFileNodePath}/{this.level}")
            || Directory.GetFiles($"{this.linkLocalFileNodeBuilding}/{this.level}").Length == 0
            || Directory.GetFiles($"{this.linkLocalFileNodePath}/{this.level}").Length == 0)
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
    public void ButtonRebootsGame()
    {
        if (Directory.Exists(this.linkLocal))
        {
            if (Directory.Exists($"{this.linkLocalFileNodeBuilding}/{this.level}"))
            {
                foreach (var file in Directory.GetFiles($"{this.linkLocalFileNodeBuilding}/{this.level}"))
                {
                    Directory.Delete(file);
                }
            }

            if (Directory.Exists($"{this.linkLocalFileNodePath}/{this.level}"))
            {
                foreach (var file in Directory.GetFiles($"{this.linkLocalFileNodePath}/{this.level}"))
                {
                    File.Delete(file);
                }
            }
        }
        SceneManager.LoadScene("Splash Scene");
    }
}
