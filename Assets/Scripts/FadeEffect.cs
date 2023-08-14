using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    private static FadeEffect instance;
    public static FadeEffect Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }
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
