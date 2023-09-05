using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public int Index;
    public int talkIndex;
    public bool isStory;
    public TextEffect te;
    public TalkManager tm;
    public Sprite[] image;
    public Image wallImage;
    public Image textImage;
    #region Singleton
    public static StoryManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    private void Update()
    {
        if(isStory)
            if (Input.GetKeyDown("space"))
            {
                Talk(Index, talkIndex);
            }
    }
    public void StartStory()
    {
        isStory = true;
        wallImage.gameObject.SetActive(true);
        textImage.gameObject.SetActive(true);
        Talk(Index, talkIndex);
        ChangeImage(Index);
    }
    void Talk(int storyIndex, int _talkIndex)
    {
        string talkData = tm.GetTalk(storyIndex, _talkIndex);
        if (talkData != null)
        {
            te.SetMsg(talkData);
        }
        else
        {
            Index++;
            isStory = false;
            talkIndex = 0;
            wallImage.gameObject.SetActive(false);
            textImage.gameObject.SetActive(false);
            GameManager.instance.isPause = false;
            return;
        }
    }
    void ChangeImage(int storyIndex)
    {
        wallImage.sprite = image[storyIndex];
    }
}
