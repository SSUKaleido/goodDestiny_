using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    public string textMsg;
    public int CharPerSeconds;
    public Text text;
    public AudioSource audioSource;
    float interval;
    int index;
    public bool isAnim;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
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
            if (textMsg[index] != ' ' || textMsg[index] != '.')
                audioSource.Play();
            index++;
            yield return new WaitForSecondsRealtime(interval);
        }
        EffectEnd();
    }

    void EffectEnd()
    {
        isAnim = false;
        text.text = textMsg;
        StoryManager.instance.talkIndex++;
    }

    public void StopTextEffect()
    {
        if (textEffectCoroutine != null)
        {
            StopCoroutine(textEffectCoroutine);
        }
    }
}
