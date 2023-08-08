using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MagicBookMove : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;

    public int next_move;
    float ATTACK_RANGE = 9f;
    float ATTACK_COOL = 4f;
    float distance;

    bool isAttacking;
    bool isCoolDown;

    GameObject magic_circle;
    Animator anim_magic;
    public Transform player;
    public GameObject bullet;


    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        magic_circle = transform.GetChild(0).gameObject;
        anim_magic = GetComponentInChildren<Animator>();
        Invoke("DecideMove", 1);
    }

    void Update()
    {
        distance = Vector2.Distance(player.position, transform.position);
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
                if (player.position.x > transform.position.x)
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
        Debug.DrawRay(rigid.position, Vector3.right, new Color(0, 1, 0));
        Debug.DrawRay(rigid.position, Vector3.left, new Color(0, 1, 0));
        RaycastHit2D ray_right = Physics2D.Raycast(rigid.position, Vector3.right, 1, LayerMask.GetMask("Platform"));
        RaycastHit2D ray_left = Physics2D.Raycast(rigid.position, Vector3.left, 1, LayerMask.GetMask("Platform"));

        if (ray_right.collider != null || ray_left.collider != null)
        {
            StartCoroutine(Move());
        }
    }

    IEnumerator Attack()
    {
        CancelInvoke("DecideMove");
        next_move = 0;
        isAttacking = true;
        anim.SetBool("IsPlayerInRange", true);
        magic_circle.SetActive(true);
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        }
        else
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        anim_magic.SetTrigger("IsBookMagic");

        yield return new WaitForSeconds(0.5f);
        GameObject alphabet = Instantiate(bullet,transform.position, transform.rotation);
        Rigidbody2D alp_rigid = alphabet.GetComponent<Rigidbody2D>();
        alp_rigid.velocity=(player.position - transform.position).normalized*8f;
        yield return new WaitForSeconds(0.833f);

        // 공격 애니메이션이 끝난 후, 쿨타임 동안 대기
        magic_circle.SetActive(false);
        isAttacking = false;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        if (player.position.x > transform.position.x)
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
    IEnumerator Move()
    {
        rigid.velocity = new Vector2(next_move, 1);
        yield return new WaitForSeconds(1);
        rigid.velocity = new Vector2(next_move, 0);
    }
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            next_move = -(next_move);
        }
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
}
