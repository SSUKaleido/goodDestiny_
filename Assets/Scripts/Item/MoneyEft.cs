using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Equipment/Money")]
public class MoneyEft : ItemEffect
{
    public int MoneyPoint;
    public override bool ExecuteRole()
    {
        GameManager.instance.roundMoney += MoneyPoint;
        return true;
    }
}
