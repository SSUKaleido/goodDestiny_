using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Equipment/AP")]
public class APEft : ItemEffect
{
    public float APPoint;
    public override bool ExecuteRole()
    {
        //Player.instance.skillDamage *= APPoint;
        return true;
    }
}
