using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHit : MonoBehaviour
{
    public int HIT_DAMAGE;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player")
        {
            GameManager.instance.TakeDamage(HIT_DAMAGE);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Ground" || other.gameObject.tag=="Wall")
        {
            StartCoroutine(DestroyBullet());
        }
    }
    IEnumerator DestroyBullet()
    {
        yield return null;
        Destroy(gameObject);
    }
}
