using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_status : MonoBehaviour
{
    public static Enemy_status instance;

    public float maxHealth;
    public float curHealth;
    public int money;
    public float damage;

    public bool isDead;

    public SpriteRenderer sprite;
    private Animator anim;

    private void Awake()
    {
        instance = this; 

        curHealth = maxHealth;
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (curHealth <= 0)
            return;
        else if (collision.tag == "Weapon")
        {
            Debug.Log("enemy: 공격당했습니다.");
            float damage = Player.instance.damage;
            curHealth -= damage;
            
            StartCoroutine(Damaged());
        }
    }

    public IEnumerator Damaged()
    {
        if (curHealth > 0)
        {
            yield return new WaitForSeconds(0.01f);
            sprite.material.color = Color.red;

            yield return new WaitForSeconds(0.3f);
            sprite.material.color = Color.white;
        }
        else
        {
            Debug.Log("Enemy: 죽었다!");
            gameObject.layer = 8;

            yield return null;

            anim.SetTrigger("Death");
            isDead = true;

            GameManager.Instance.roundMoney += money;

            Destroy(gameObject, 3);
        }
    }


}
