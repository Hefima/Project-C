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
}

public class DebugManager : MonoBehaviour
{
    public bool DefaultDebugs = true;
    public bool PlayerDebugs = true;
    public bool ItemDebugs = true;
    public bool EnemyDebug = true;

    public void DebugLog(string _content, DebugType _type)
    {
        if (!AskTypeAllowed(_type))
            return;
        Debug.Log(_type + ": " + _content);
    }

    public void DebugLog(string _content)
    {
        if (!AskTypeAllowed(DebugType.DEFAULTDEBUG))
            return;
        Debug.Log(DebugType.DEFAULTDEBUG + ": " + _content);
    }

    public void DebugLogWarning(string _content, DebugType _type)
    {
        if (!AskTypeAllowed(_type))
            return;
        Debug.LogWarning(_type + ": " + _content);
    }

    public void DebugLogWarning(string _content)
    {
        if (!AskTypeAllowed(DebugType.DEFAULTDEBUG))
            return;
        Debug.LogWarning(DebugType.DEFAULTDEBUG + ": " + _content);
    }

    bool AskTypeAllowed(DebugType _type)
    {
        switch (_type)
        {
            case DebugType.DEFAULTDEBUG:
                if (DefaultDebugs)
                    return true;
                break;
            case DebugType.PLAYERDEBUG:
                if (PlayerDebugs)
                    return true;
                break;
            case DebugType.ITEMDEBUG:
                if (ItemDebugs)
                    return true;
                break;
            case DebugType.ENEMYDEBUG:
                if (EnemyDebug)
                    return true;
                break;
            default:
                return true;
        }
        return false;
    }
}
