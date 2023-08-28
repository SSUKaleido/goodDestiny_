using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewManager : MonoBehaviour
{
    public int enemyKill;
    public Text text;
    private static ReviewManager instance;
    public static ReviewManager Instance
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
        text.text = "";
    }
    void Update()
    {
        if (GameManager.Instance.isGameOver == true)
        {
            DeathUI();
        }
    }
    void DeathUI()
    {
        text.text = ((int)Mathf.Round(GameManager.Instance.stopWatch)) / 60 + ":" + 
                ((int)Mathf.Round(GameManager.Instance.stopWatch)) % 60 + "\n"
                   + enemyKill + "\n" + GameManager.Instance.roundMoney;
    }
}
