using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ItemEft/Equipment/HP")]
public class HPEft : ItemEffect
{
    public int hpPoint;
    public override bool ExecuteRole()
    {
        GameManager.instance.max_health += hpPoint;
        GameManager.instance.cur_health += hpPoint;
        return true;
    }
}
