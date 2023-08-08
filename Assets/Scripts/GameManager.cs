using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public int stage_row;
    public int stage_column;

    public int max_health;
    public int cur_health;
    public int player_money;

    public bool is_gameover;
    public bool is_pause;
    public GameObject gameoverUI;
    public GameObject Menuset;
    public GameObject[] stage1;
    public GameObject[] stage2;
    public GameObject[] stage3;
    public GameObject[] boss_stage;

    void Start()
    {
        max_health = 100;
        cur_health = 100;
        player_money = 0;
        stage_column = 0;
    }

    void Update()
    {
        max_health = 100;
        cur_health = 100;
        player_money = 0;
        stage_column = 0;
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
    public void NextStage()
    {
        switch (stage_column) {
            case 1:
                if (stage_row != 3) //¿œπ› ∏  ¿Ø¡ˆ
                {
                    stage1[stage_row++].SetActive(false);
                    stage1[stage_row].SetActive(true);
                }
                else //∫∏Ω∫ ∏  ¿Ãµø
                {
                    stage1[stage_row].SetActive(false);
                    stage_row = 0;
                    boss_stage[stage_column++].SetActive(true);
                }
                break;
            case 2:
                if (stage_row != 3) //¿œπ› ∏  ¿Ø¡ˆ
                {
                    stage2[stage_row++].SetActive(false);
                    stage2[stage_row].SetActive(true);
                }
                else //∫∏Ω∫ ∏  ¿Ãµø
                {
                    stage2[stage_row].SetActive(false);
                    stage_row = 0;
                    boss_stage[stage_column++].SetActive(true);
                }
                break;
            case 3:
                if (stage_row != 3) //¿œπ› ∏  ¿Ø¡ˆ
                {
                    stage3[stage_row++].SetActive(false);
                    stage3[stage_row].SetActive(true);
                }
                else //∫∏Ω∫ ∏  ¿Ãµø
                {
                    stage3[stage_row].SetActive(false);
                    stage_row = 0;
                    boss_stage[stage_column++].SetActive(true);
                }
                break;
        }
    }
    public void NextScene()
    {

    }
    public void PlayerDead()
    {
        is_gameover = true;
        gameoverUI.SetActive(true);
    }
}
