using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager acc;

    public InputKeys IK;
    public UIManager UI;
    public EventManager EM;
    public ItemManager IM;
    public SceneHandler SH;

    private void Awake()
    {
        acc = this;
    }

    private void Update()
    {
        UI.toolTip.FollowMouse();
    }
}
