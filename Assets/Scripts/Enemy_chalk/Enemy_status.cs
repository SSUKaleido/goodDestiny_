using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_status : MonoBehaviour
{
    public static Enemy_status instance;

    private float maxHealth;
    public float curHealth;
    public int money;
    public float damage;

    public bool isDead;

    private Rigidbody rb;
    private Animator anim;

    private void Awake()
    {
        instance = this; 

        maxHealth = curHealth;
        rb = GetComponentInParent<Rigidbody>();
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
            yield return new WaitForSeconds(0.1f);

            anim.SetTrigger("Damaged");
        }
        else
        {
            Debug.Log("Enemy: 죽었다!");
            gameObject.layer = 8;

            yield return null;

            anim.SetTrigger("Death");
            isDead = true;

            GameManager.Instance.GetMoney(money);

            Destroy(gameObject, 3);
        }
    }


}
