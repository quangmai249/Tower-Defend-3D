using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDesign : MonoBehaviour
{
    [SerializeField] Level level = Level.LEVEL_1;
    [SerializeField] TextMeshProUGUI textLevel;
    [SerializeField] bool checking = false;
    [SerializeField] LineRenderer lineRenderer;

    private string textLv;
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
        if (this.checking == true)
        {
            this.textLevel.text = level.ToString().Replace('_', ' ');
            this.textLv = level.ToString();
        }
        else
        {
            this.textLevel.text = PlayerPrefs.GetString("LEVEL").Replace('_', ' ');
            this.textLv = PlayerPrefs.GetString("LEVEL");
        }

        StartCoroutine(nameof(this.DrawPathLine));
    }
    IEnumerator DrawPathLine()
    {
        List<LineRenderer> ls = new List<LineRenderer>();

        yield return new WaitForSeconds(6f);

        for (int i = 0; i < Directory.GetFiles(FileLocalLink.UserFolderNodePath + this.textLv).Length; i++)
        {
            yield return new WaitForSeconds(1f);

            FilePath f = new FilePath(FileLocalLink.UserFolderNodePath + this.textLv, this.textLv + (i + 1));
            List<Vector3> vec = new List<Vector3>();

            foreach (Vector3 temp in f.ReadFromFile().ToList())
                vec.Add(temp + new Vector3(0, 0.1f, 0));

            LineRenderer lineRenderer_new = Instantiate(lineRenderer);

            lineRenderer_new.startWidth = 2f;
            lineRenderer_new.endWidth = 2f;

            lineRenderer_new.gameObject.transform.SetParent(this.gameObject.transform);
            lineRenderer_new.positionCount = vec.Count;
            lineRenderer_new.SetPositions(vec.ToArray());

            ls.Add(lineRenderer_new);
        }

        StartCoroutine(nameof(this.CoroutineEnablePathline), ls);
    }
    IEnumerator CoroutineEnablePathline(List<LineRenderer> ls)
    {
        yield return new WaitForSeconds(.5f);
        this.EnablePathLine(ls, false);
        yield return new WaitForSeconds(.5f);
        this.EnablePathLine(ls, true);
        yield return new WaitForSeconds(.5f);
        this.EnablePathLine(ls, false);
        yield return new WaitForSeconds(.5f);
        this.EnablePathLine(ls, true);
        yield return new WaitForSeconds(.5f);
        this.EnablePathLine(ls, false);
        yield return new WaitForSeconds(.5f);
        this.EnablePathLine(ls, true);
    }
    private void EnablePathLine(List<LineRenderer> ls, bool b)
    {
        foreach (LineRenderer lineRenderer in ls)
            lineRenderer.enabled = b;
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
            if (Directory.Exists(FileLocalLink.UserFolderNodeBuilding + this.textLv))
            {
                foreach (var file in Directory.GetFiles(FileLocalLink.UserFolderNodeBuilding + this.textLv))
                {
                    File.Delete(file);
                }
            }

            if (Directory.Exists(FileLocalLink.UserFolderNodePath + this.textLv))
            {
                foreach (var file in Directory.GetFiles(FileLocalLink.UserFolderNodePath + this.textLv))
                {
                    File.Delete(file);
                }
            }
        }

        PlayerPrefs.SetInt($"LEVEL_1", 1);
        PlayerPrefs.Save();

        for (int i = 2; i <= Enum.GetValues(typeof(Level)).Length; i++)
        {
            PlayerPrefs.SetInt($"LEVEL_{i}", 0);
            PlayerPrefs.Save();
        }


        SceneManager.LoadScene(SceneNameManager.SceneSplash);
    }
}
