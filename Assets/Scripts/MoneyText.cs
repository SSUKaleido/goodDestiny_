using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyText : MonoBehaviour
{
    GameManager gm;
    public Text ScriptTxt;
    public int player_money;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player_money = gm.roundMoney;
        ScriptTxt.text = ""+player_money;
    }
    void Update()
    {
        player_money = gm.roundMoney;
        ScriptTxt.text = "" + player_money;
    }
}
