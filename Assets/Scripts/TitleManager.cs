using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public FadeEffect fe;
    public void GameStart()
    {
        StartCoroutine(StartEffect());
    }

    public void FadeEffect()
    {
        StartCoroutine(fe.InGradation());
    }
    IEnumerator StartEffect()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Chapter1");
    }
}
