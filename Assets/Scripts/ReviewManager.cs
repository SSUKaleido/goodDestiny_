using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewManager : MonoBehaviour
{
    public int enemyKill;
    public Text text;
    #region Singleton
    public static ReviewManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    void Start()
    {
        text.text = "";
    }
    void Update()
    {
        if (GameManager.instance.isGameOver == true)
        {
            DeathUI();
        }
    }
    void DeathUI()
    {
        text.text = ((int)Mathf.Round(GameManager.instance.stopWatch)) / 60 + ":" + 
                ((int)Mathf.Round(GameManager.instance.stopWatch)) % 60 + "\n"
                   + enemyKill + "\n" + GameManager.instance.roundMoney;
    }
}
