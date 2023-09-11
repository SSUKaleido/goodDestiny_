using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    #region Singleton
    public static FadeEffect instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public IEnumerator InGradation()
    {
        gameObject.SetActive(true);
        float a;
        float t = 100;
        for (a = 0; a < t; a++)
        {
            GetComponent<Image>().color = new Color(0, 0, 0,a/t);
            yield return new WaitForSeconds(0.01f);
        }
    }
    public IEnumerator OutGradation()
    {
        float a;
        float t = 100;
        for (a = t; a > 0; a--)
        {
            GetComponent<Image>().color = new Color(0, 0, 0, a/t);
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.SetActive(false);
    }
}
