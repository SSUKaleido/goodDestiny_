using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public int stage_count;
    public int chapter_count;
    public float max_health;
    public float cur_health;
    public int roundMoney;
    public int totalMoney;
    public float stopWatch;

    public bool isGameOver;
    public bool isStageChange;
    public bool isPause;
    public bool isText;

    public GameObject player;
    public FadeEffect fe;
    public GameObject gameoverUI;
    public GameObject pauseUI;
    public GameObject talkUI;

    public GameObject[] stageIndex1;
    public GameObject[] stageIndex2;
    public GameObject[] bossStage;
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
        PlayerPrefs.GetInt("stage_count", stage_count);
        PlayerPrefs.GetInt("chapter_count", chapter_count);
        PlayerPrefs.GetFloat("max_health", max_health);
        PlayerPrefs.GetFloat("cur_health", cur_health);
        PlayerPrefs.GetInt("roundMoney", roundMoney);
        PlayerPrefs.GetInt("totalMoney", totalMoney);
        PlayerPrefs.GetFloat("stopWatch", stopWatch);
    }
    #endregion
    void Start()
    {
        GoStory();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        MenuSet();
        if (!isGameOver)
            stopWatch += Time.deltaTime;
    }
    void MenuSet()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseUI.activeSelf)
            {
                isPause = false;
                pauseUI.SetActive(false);
            }
            else
            {
                isPause = true;
                pauseUI.SetActive(true);
            }
        }
        if (isPause == false)
        {
            Time.timeScale = 1;
        }
        else
            Time.timeScale = 0;
    }
    public void ChangePause()
    {
        isPause = false;
    }
    public IEnumerator NextStage()
    {
        yield return StartCoroutine(fe.InGradation());
        player.transform.position = new Vector3(-6, -2, 0);
        switch (chapter_count)
        {
            case 0:
                if (stage_count < 2) 
                {
                    stageIndex1[stage_count++].SetActive(false);
                    stageIndex1[stage_count].SetActive(true);
                }
                else 
                {
                    stageIndex1[stage_count].SetActive(false);
                    stage_count = 0;
                    bossStage[chapter_count++].SetActive(true);
                    GoStory();
                }
                break;
            case 1:
                if (stage_count == 0)
                {
                    bossStage[0].SetActive(false);
                    stageIndex2[0].SetActive(true);
                    stage_count++;
                }
                else if (stage_count ==1) 
                {
                    stageIndex2[0].SetActive(false);
                    stage_count++;
                    stageIndex2[1].SetActive(true);
                }
                else if (stage_count == 2)
                {
                    stageIndex2[1].SetActive(false);
                    stage_count++;
                    stageIndex2[2].SetActive(true);
                }
                else 
                {
                    stageIndex2[2].SetActive(false);
                    bossStage[chapter_count].SetActive(true);
                    GoStory();
                }
                break;
        }
        yield return StartCoroutine(fe.OutGradation());
    }
    public void GoStory()
    {
        isPause = true;
        StoryManager.instance.StartStory();
    }

    private void DataSave()
    {
        PlayerPrefs.SetInt("stage_count", stage_count);
        PlayerPrefs.SetInt("chapter_count", chapter_count);
        PlayerPrefs.SetFloat("max_health", max_health);
        PlayerPrefs.SetFloat("cur_health", cur_health);
        PlayerPrefs.SetInt("roundMoney", roundMoney);
        PlayerPrefs.SetInt("totalMoney", totalMoney);
        PlayerPrefs.SetFloat("stopWatch", stopWatch);
    }

    public void GoMain()
    {
        DataSave();
        SceneManager.LoadScene("Main");
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("player: 데미지를 입었다!");
        if (cur_health > 0)
        {
            cur_health -= damage;
        }
        else
            StartCoroutine(PlayerDead());
    }


    IEnumerator PlayerDead()
    {
        isGameOver = true;
        yield return new WaitForSeconds(2);
        isPause = true;
        gameoverUI.SetActive(true);
        totalMoney += roundMoney;
    }
    public void GetMoney(int money)
    {
        roundMoney += money;
    }
}