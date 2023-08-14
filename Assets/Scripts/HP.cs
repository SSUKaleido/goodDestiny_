using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    GameManager gm;
    public Text ScriptTxt;
    public float max_health;
    public float cur_health;
    public Image health_bar;
    
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cur_health = 100;
        max_health = 100;
        ScriptTxt.text = cur_health+"/"+max_health;
    }
    void Update()
    {
        max_health = gm.max_health;
        cur_health = gm.cur_health;
        ScriptTxt.text = cur_health + "/" + max_health;
        health_bar.fillAmount = cur_health / max_health;
    }

}
