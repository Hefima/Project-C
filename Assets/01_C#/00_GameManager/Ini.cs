using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ini : MonoBehaviour
{
    float waitTime = 2;
    private void Start()
    {
        StartCoroutine(Ini_Courutine());
    }

    IEnumerator Ini_Courutine()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(1);
    }
}
