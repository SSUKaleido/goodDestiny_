using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainMonster : MonoBehaviour
{
    public int remainMonster;
    public GameObject goalObject;
    public ItemManager im;
    public void MonsterDied()
    {
        remainMonster--;
        ReviewManager.instance.enemyKill++;
        if (remainMonster <= 0)
            OpenPortal();
    }
    void OpenPortal()
    {
        goalObject.SetActive(true);
        im.GenerateItem();
    }
}
