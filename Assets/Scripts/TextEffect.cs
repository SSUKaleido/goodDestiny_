using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    public string textMsg;
    public int CharPerSeconds;
    public Text text;
    float interval;
    int index;
    public bool isAnim;

    private Coroutine textEffectCoroutine;

    public void SetMsg(string msg)
    {
        if (isAnim)
        {
            StopTextEffect();
            EffectEnd();
        }
        else
        {
            textMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        text.text = "";
        index = 0;
        interval = 1.0f / CharPerSeconds;
        isAnim = true;
        textEffectCoroutine = StartCoroutine(Effecting());
    }

    IEnumerator Effecting()
    {
        while (index < textMsg.Length)
        {
            text.text += textMsg[index];
            index++;
            yield return new WaitForSecondsRealtime(interval);
        }

        EffectEnd();
    }

    void EffectEnd()
    {
        isAnim = false;
        text.text = textMsg;
        StoryManager.instance.talkIndex++; // 이 부분을 필요에 따라 활성화하세요.
    }

    public void StopTextEffect()
    {
        if (textEffectCoroutine != null)
        {
            StopCoroutine(textEffectCoroutine);
        }
    }
}
