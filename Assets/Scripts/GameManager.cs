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

    public float max_health;
    public float cur_health;
    public int player_money;

    public bool is_gameover;
    public bool is_pause;
    public FadeEffect fe;
    public GameObject gameoverUI;
    public GameObject Menuset;
    public GameObject[] chapter1;
    public GameObject[] chapter2;
    public GameObject[] chapter3;
    public GameObject[] boss_stage;
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
        player_money = 0;
        chapter_count = 1;
        stage_count = 0;
    }

    void Update()
    {
        
        if (Input.GetButtonDown("Vertical"))
            StartCoroutine(NextStage());
        
        if (Input.GetButtonDown("Cancel"))
        {
            if (Menuset.activeSelf)
            {
                is_pause = false;
                Time.timeScale = 1;
                Menuset.SetActive(false);
            }
            else
            {
                is_pause = true;
                Time.timeScale = 0;
                Menuset.SetActive(true);
            }
        }
        if (is_pause==false)
        {
            Time.timeScale = 1;
        }

    }
    public void ChangePause()
    {
        is_pause = false;
    }
    IEnumerator NextStage()
    {
        yield return StartCoroutine(fe.InGradation());
        switch (chapter_count) {
            case 1:
                if (stage_count != 3) //ÀÏ¹Ý ¸Ê À¯Áö
                {
                    chapter1[stage_count++].SetActive(false);
                    chapter1[stage_count].SetActive(true);
                }
                else //º¸½º ¸Ê ÀÌµ¿
                {
                    chapter1[stage_count].SetActive(false);
                    stage_count = 0;
                    boss_stage[chapter_count++].SetActive(true);
                }
                break;
            case 2:
                if (stage_count != 3) //ÀÏ¹Ý ¸Ê À¯Áö
                {
                    chapter2[stage_count++].SetActive(false);
                    chapter2[stage_count].SetActive(true);
                }
                else //º¸½º ¸Ê ÀÌµ¿
                {
                    chapter2[stage_count].SetActive(false);
                    stage_count = 0;
                    boss_stage[chapter_count++].SetActive(true);
                }
                break;
            case 3:
                if (stage_count != 3) //ÀÏ¹Ý ¸Ê À¯Áö
                {
                    chapter3[stage_count++].SetActive(false);
                    chapter3[stage_count].SetActive(true);
                }
                else //º¸½º ¸Ê ÀÌµ¿
                {
                    chapter3[stage_count].SetActive(false);
                    stage_count = 0;
                    boss_stage[chapter_count++].SetActive(true);
                }
                break;
        }
        yield return StartCoroutine(fe.OutGradation());
    }
    public void NextScene()
    {/*
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
        */
    }
    public void TakeDamage(int damage)
    {
        if (cur_health > 0)
        {
            cur_health -= damage;
        }
        else
            PlayerDead();
    }
    public void PlayerDead()
    {
        is_gameover = true;
        gameoverUI.SetActive(true);
    }
}
