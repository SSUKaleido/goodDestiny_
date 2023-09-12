using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!Player.instance.isDamage)
            {
                float damage = GetComponentInParent<Enemy_behaviour>().damage;
                GameManager.instance.TakeDamage(damage);
            }
        }
    }
}
