using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishControl : MonoBehaviour
{
    public int remainMonster;
    public bool isBossroom;
    public bool isPlayer = false;
    private void Update()
    {
        Portal();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayer = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayer = false;
        }
    }
    public void MonsterDied()
    {
        remainMonster--;
        ReviewManager.instance.enemyKill++;
        if (remainMonster <= 0)
            OpenPortal();
    }
    void OpenPortal()
    {
        gameObject.SetActive(true);
    }

    void Portal()
    {
        if (Input.GetButtonDown("Vertical") && isPlayer)
        {
            if (isBossroom) 
            {
                GameManager.instance.GoStory();
            }
            else
                StartCoroutine(GameManager.instance.NextStage());
        }
    }
}
