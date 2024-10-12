using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClass
{
    private string nameLevel;
    private bool isUnlock;
    public LevelClass() { }
    public LevelClass(string nameLevel, bool isUnlock)
    {
        this.nameLevel = nameLevel;
        this.isUnlock = isUnlock;
    }
    public void SetNameLevel(string s)
    {
        this.nameLevel = s;
    }
    public void SetIsUnlock(bool b)
    {
        this.isUnlock = b;
    }
    public string GetNameLevel()
    {
        return this.nameLevel;
    }
    public bool GetIsUnlock()
    {
        return this.isUnlock;
    }
}
