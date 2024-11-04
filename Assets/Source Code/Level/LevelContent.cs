using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelContent : MonoBehaviour
{
    [SerializeField] GameObject btnLevel;
    [SerializeField] TextMeshProUGUI textButtonPlayGame;
    [SerializeField] List<GameObject> lsButtonLevel;
    [SerializeField] int numPool = 50;
    private GameObject _btnLevel;
    public static LevelContent Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"{this.gameObject.name} is NOT NULL!");
            return;
        }
        Instance = this;

        CreateObjectPooling(this.numPool);
    }
    private void Start()
    {
        PlayerPrefs.SetString("LEVEL", string.Empty);
        PlayerPrefs.Save();

        this.textButtonPlayGame.text = "NONE";

        foreach (var item in Enum.GetValues(typeof(Level)))
        {
            foreach (var temp in lsButtonLevel)
            {
                if (item.ToString().Equals(temp.gameObject.name.ToString()) && PlayerPrefs.GetInt(item.ToString()) == 1)
                {
                    temp.gameObject.SetActive(true);
                    temp.GetComponentInChildren<TextMeshProUGUI>().text = temp.gameObject.name;
                    break;
                }
            }
        }
    }
    private void CreateObjectPooling(int defaultNumPooling)
    {
        //for (int i = 1; i <= defaultNumPooling; i++)
        //{
        //    PlayerPrefs.SetInt($"LEVEL_{i}", 0);
        //    PlayerPrefs.Save();
        //}

        for (int i = 1; i <= defaultNumPooling; i++)
        {
            this._btnLevel = Instantiate(this.btnLevel);
            this._btnLevel.gameObject.name = $"LEVEL_{i}";
            this._btnLevel.gameObject.transform.SetParent(this.gameObject.transform);
            this._btnLevel.SetActive(false);

            if (PlayerPrefs.GetInt(this._btnLevel.gameObject.name) == 1)
                this._btnLevel.SetActive(true);

            lsButtonLevel.Add(this._btnLevel);
        }

        for (int i = 1; i < 4; i++)
        {
            PlayerPrefs.SetInt($"LEVEL_{i}", 1);
            PlayerPrefs.Save();
        }
    }
    public void SetTextButtonPlayGame(string text)
    {
        this.textButtonPlayGame.text = text;
    }
}
