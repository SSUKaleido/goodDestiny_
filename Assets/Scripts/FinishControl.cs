using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishControl : MonoBehaviour
{
    public bool isBossroom;
    public bool isPlayer = false;
    private void Awake()
    {
        
    }
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

    void Portal()
    {
        if (Input.GetButtonDown("Vertical") && isPlayer)
        {
            AudioManager.instance.PlaySFX("NextStage");
            if (isBossroom)
            {
                GameManager.instance.GoStory();
            }
            else
            {
                StartCoroutine(GameManager.instance.NextStage());
            }
        }
    }
}
