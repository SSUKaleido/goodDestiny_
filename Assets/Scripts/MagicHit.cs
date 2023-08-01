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
            /*플레이어에게 데미지
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
            */
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "TileMap")
        {
            StartCoroutine(DestroyBullet());
        }
    }
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
