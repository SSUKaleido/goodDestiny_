using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type attackType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (attackType == Type.Melee)
            {
                if (!Player.instance.isDamage)
                {
                    float damage = GetComponentInParent<Enemy_behaviour>().damage;
                    GameManager.instance.TakeDamage(damage);
                }
            }
            
            if(attackType == Type.Range)
            {
                if (!Player.instance.isDamage)
                {
                    float damage = GameObject.Find("Boss2").GetComponent<Enemy_behaviour>().slashDamage;
                    GameManager.instance.TakeDamage(damage);
                }

                if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Wall")
                    StartCoroutine(DestroyBullet());
            }
                
        }
    }
    IEnumerator DestroyBullet()
    {
        yield return null;
        Destroy(gameObject);
    }
}
