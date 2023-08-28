using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Equipment/Cool")]
public class CoolEft : ItemEffect
{
    public float coolPoint;
    public override bool ExecuteRole()
    {
        //player.skillCool *= coolPoint;
        return true;
    }
}
