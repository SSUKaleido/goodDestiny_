using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ItemEft/Equipment/HP")]
public class HPEft : ItemEffect
{
    public int hpPoint;
    public override bool ExecuteRole()
    {
        Debug.Log("t");
        GameManager.Instance.max_health += hpPoint;
        GameManager.Instance.cur_health += hpPoint;
        return true;
    }
}
