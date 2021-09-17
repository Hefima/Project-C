using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsPlayer : MonoBehaviour
{
    public TestMove testMove;
    

    public void StopMove()
    {
        print("test");
        if (testMove.moveAllowed)
            testMove.moveAllowed = false;
        else
            testMove.moveAllowed = true;
    }
}
