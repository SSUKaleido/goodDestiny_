using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PhysicBookHit : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;

    public int next_move;
    float ATTACK_RANGE = 2f;
    float ATTACK_COOL = 2f;
    float distance;
    public int HIT_DAMAGE;
    public int max_health = 20;
    public int cur_health;

    bool isAttacking;
    bool isCoolDown;

    public FinishControl fc;
    SpriteRenderer spriteRenderer;
    GameObject effect;
    Animator anim_effect;
    public Transform player;


    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        effect = transform.GetChild(0).gameObject;
        anim_effect = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("DecideMove", 1);

        cur_health = max_health;
    }

    void Update()
    {
        distance = Vector2.Distance(player.position, transform.position);
        if (distance <= ATTACK_RANGE)
        {
            if (!isAttacking && !isCoolDown)
            {
                anim.SetBool("IsPlayerClosed", true);
                StartCoroutine(Attack());
            }
            else if (isCoolDown)
            {
                anim.SetBool("IsPlayerInRange", false);
            }
            else if (!isAttacking)
            {
                anim.SetBool("IsPlayerInRange", false);
                if (player.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(0.5f, 0.5f, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-0.5f, 0.5f, 1);
                }
            }
        }
    }

    IEnumerator Attack()
    {
        CancelInvoke("DecideMove");
        next_move = 0;
        isAttacking = true;
        anim.SetBool("IsPlayerInRange", true);
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
            anim.SetTrigger("Left");
        }
        else
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
            anim.SetTrigger("Right");
        }
        yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(0.2f);
        effect.SetActive(true);
        anim_effect.SetTrigger("IsBookAttack");
        yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(0.2f);
        effect.SetActive(false);

        // 공격 애니메이션이 끝난 후, 쿨타임 동안 대기합니다.
        isAttacking = false;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        else
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        }

        isCoolDown = true;
        anim.SetBool("IsPlayerInRange", false);

        yield return new WaitForSeconds(ATTACK_COOL);
        isCoolDown = false;
    }

    public void OnDamage(int damage)
    {
        cur_health -= damage;
        StartCoroutine(OnDamage());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
            GameManager.Instance.TakeDamage(HIT_DAMAGE);
    }
    IEnumerator OnDamage()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        if (cur_health > 0)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.gray;
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
        GameManager.Instance.roundMoney += 100;
        fc.MonsterDied();
    }
}
