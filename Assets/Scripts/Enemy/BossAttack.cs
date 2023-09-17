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
        InvokeRepeating("MonsterAttack", 0f, 3f);
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
