using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject bullet;
    public GameObject diagonalbullet;
    public Transform bulletPos;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Enemy_behaviour>().phase2 == true)
            InvokeRepeating("MonsterAttack", 0f, 20f);
    }
    void MonsterAttack()
    {
        Instantiate(bullet, bulletPos.position, transform.rotation);
        Instantiate(diagonalbullet, bulletPos.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
