using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCount : MonoBehaviour
{
    public int remainMonster;
    public GameObject goal;
    public void MonsterDied()
    {
        remainMonster--;
        if (remainMonster <= 0)
            OpenPortal();
    }
    void OpenPortal()
    {
        goal.SetActive(true);
    }
}
