using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DebugType
{
    DEFAULTDEBUG,
    PLAYERDEBUG,
    ITEMDEBUG,
    ENEMYDEBUG,
    QUESTDEBUG,
}

[System.Serializable]
public class DebugClass
{
    public DebugType Type;
    public bool IsActive;
}
[ExecuteAlways]
public class DebugManager : MonoBehaviour
{
    public static DebugManager instance;
    public bool createDebugs;
    public List<DebugClass> DebugList = new List<DebugClass>();


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {

        if (createDebugs)
        {
            CreateDebugs();
            createDebugs = false;
        }
    }

    public void CreateDebugs()
    {
        DebugList.Clear();
        for (int i = 0; i < Enum.GetValues(typeof(DebugType)).Length; i++)
        {
            DebugList.Add( new DebugClass() {Type = (DebugType)i, IsActive = true});
        }
    }

    public static void DebugLog(string _content, DebugType _type = DebugType.DEFAULTDEBUG)
    {
        if (!AskTypeAllowed(_type))
            return;
        Debug.Log(_type + ": " + _content);
    }

    public static void DebugLogWarning(string _content, DebugType _type = DebugType.DEFAULTDEBUG)
    {
        if (!AskTypeAllowed(_type))
            return;
        Debug.LogWarning(_type + ": " + _content);
    }

    static bool AskTypeAllowed(DebugType _type)
    {
        for (int i = 0; i < instance.DebugList.Count; i++)
        {
            if(instance.DebugList[i].Type == _type && instance.DebugList[i].IsActive)
            {
                return true;
            }
        }
        return false;
    }
}
