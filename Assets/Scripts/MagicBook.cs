using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MagicBook : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;

    public float max_health;
    public float cur_health;
    public int next_move;
    float ATTACK_RANGE = 9f;
    float ATTACK_COOL = 4f;
    float distance;

    bool isAttacking;
    bool isCoolDown;

    public RemainMonster rm;

    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    GameObject magic_circle;
    Animator anim_magic;
    public GameObject bullet;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        magic_circle = transform.GetChild(0).gameObject;
        anim_magic = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("DecideMove", 1);
    }

    void FixedUpdate()
    {
        distance = Vector2.Distance(Player.instance.transform.position, transform.position);
        rigid.velocity = new Vector2(next_move, rigid.velocity.y);
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
                if (Player.instance.transform.position.x > transform.position.x)
                {
                    next_move = 1;
                    transform.localScale = new Vector3(0.5f, 0.5f,1);
                }
                else
                {
                    next_move = -1;
                    transform.localScale = new Vector3(-0.5f, 0.5f, 1);
                }
            }
        }
        RayUse();
    }
    void RayUse()
    {
        Debug.DrawRay(rigid.position, Vector3.right, new Color(0, 1, 0));
        Debug.DrawRay(rigid.position, Vector3.left, new Color(0, 1, 0));
        RaycastHit2D ray_right = Physics2D.Raycast(rigid.position, Vector3.right, 1, LayerMask.GetMask("Platform"));
        RaycastHit2D ray_left = Physics2D.Raycast(rigid.position, Vector3.left, 1, LayerMask.GetMask("Platform"));

        if (ray_right.collider != null || ray_left.collider != null)
        {
            YMove();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            cur_health -= Player.instance.damage;
            StartCoroutine(OnDamage(Player.instance.damage));
        }
        
    }
    IEnumerator OnDamage(float damage)
    {
        spriteRenderer.color = Color.red;
        AudioManager.instance.PlaySFX("Attack");
        cur_health -= damage;
        yield return new WaitForSeconds(0.1f);
        if (cur_health > 0)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.gray;
            yield return new WaitForSeconds(0.2f);
            Die();
        }
    }

    IEnumerator Attack()
    {
        CancelInvoke("DecideMove");
        next_move = 0;
        isAttacking = true;
        anim.SetBool("IsPlayerInRange", true);
        magic_circle.SetActive(true);
        if (Player.instance.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        }
        else
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        anim_magic.SetTrigger("IsBookMagic");

        yield return new WaitForSeconds(0.5f);
        GameObject alphabet = Instantiate(bullet,transform.position,transform.rotation);
        Rigidbody2D alp_rigid = alphabet.GetComponent<Rigidbody2D>();
        audioSource.Play();
        alp_rigid.velocity=(Player.instance.transform.position - transform.position).normalized*8f;
        yield return new WaitForSeconds(0.833f);

        // 공격 애니메이션이 끝난 후, 쿨타임 동안 대기
        magic_circle.SetActive(false);
        isAttacking = false;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        if (Player.instance.transform.position.x > transform.position.x)
        {
            next_move = 1;
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        else
        {
            next_move = -1;
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        }

        isCoolDown = true;
        anim.SetBool("IsPlayerInRange", false);

        yield return new WaitForSeconds(ATTACK_COOL);
        isCoolDown = false;
    }
    void YMove()
    {
        next_move = -(next_move);
    }
    
    void DecideMove()
    {
        next_move = Random.Range(-1, 2);
        if (next_move > 0)
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        else
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        if (!anim.GetBool("IsPlayerClosed"))
        {
            Invoke("DecideMove", 4);
        }
    }

    void Die()
    {
        Destroy(gameObject);
        GameManager.instance.roundMoney += 100;
        rm.MonsterDied();
    }
}
