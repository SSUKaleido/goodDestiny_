using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int stage_count;
    public int chapter_count;
    public int max_health;
    public int cur_health;
    public int roundMoney;
    public int totalMoney;
    public float stopWatch;

    public bool isGameOver=false;
    public bool isPause=false;
    public bool isText=false;

    public GameObject player;
    public FadeEffect fe;

    public GameObject gameoverUI;
    public GameObject pauseUI;

    public GameObject[] chapter1;
    public GameObject[] chapter2;
    public GameObject[] chapter3;
    public GameObject[] bossStage;

    private static GameManager instance;
    public static GameManager Instance
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
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }
    void Start()
    {
        max_health = 100;
        cur_health = 100;
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
        if (Input.GetButtonDown("Cancel"))
        {
            if (pauseUI.activeSelf)
            {
                isPause = false;
                Time.timeScale = 1;
                pauseUI.SetActive(false);
            }
            else
            {
                isPause = true;
                Time.timeScale = 0;
                pauseUI.SetActive(true);
            }
        }
        if (isPause == false)
        {
            Time.timeScale = 1;
        }
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
                if (stage_count < 2) //ÀÏ¹Ý ¸Ê À¯Áö
                {
                    chapter1[stage_count++].SetActive(false);
                    chapter1[stage_count].SetActive(true);
                }
                else //º¸½º ¸Ê ÀÌµ¿
                {
                    chapter1[stage_count].SetActive(false);
                    stage_count = 0;
                    bossStage[chapter_count++].SetActive(true);
                }
                break;
            case 1:
                if (stage_count < 2) //ÀÏ¹Ý ¸Ê À¯Áö
                {
                    chapter2[stage_count++].SetActive(false);
                    chapter2[stage_count].SetActive(true);
                }
                else //º¸½º ¸Ê ÀÌµ¿
                {
                    chapter2[stage_count].SetActive(false);
                    stage_count = 0;
                    bossStage[chapter_count++].SetActive(true);
                }
                break;
            case 2:
                if (stage_count < 2) //ÀÏ¹Ý ¸Ê À¯Áö
                {
                    chapter3[stage_count++].SetActive(false);
                    chapter3[stage_count].SetActive(true);
                }
                else //º¸½º ¸Ê ÀÌµ¿
                {
                    chapter3[stage_count].SetActive(false);
                    stage_count = 0;
                    bossStage[chapter_count++].SetActive(true);
                }
                break;
        }
        yield return StartCoroutine(fe.OutGradation());
    }
    public void NextScene()
    {
        switch (chapter_count)
        {
            case 1:
                SceneManager.LoadScene("Chapter1");
                break;
            case 2:
                SceneManager.LoadScene("Chapter2");
                break;
            case 3:
                SceneManager.LoadScene("Chapter3");
                break;
        }
    }
    public void GoMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void TakeDamage(int damage)
    {
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
        Time.timeScale = 0;
        gameoverUI.SetActive(true);
        totalMoney += roundMoney;
    }
}
    
