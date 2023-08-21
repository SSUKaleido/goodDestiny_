using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class PhysicBookMoves : MonoBehaviour
{
    Animator book_anim;
    Rigidbody2D rigid;

    public int next_move;
    float ATTACK_RANGE = 2f;
    float ATTACK_COOL = 2f;
    float distance;

    bool isAttacking;
    bool isCoolDown;
    
    public Transform player;
    void Awake()
    {
        book_anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
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
                StartCoroutine(Attack());
            }
            else if (isCoolDown)
            {
            }
            else if (!isAttacking)
            {
                if (player.position.x > transform.position.x)
                {
                    next_move = 1;
                }
                else
                {
                    next_move = -1;
                }
            }
        }
        else 
        {
            if (player.position.x > transform.position.x)
            {
                next_move = 1;
            }
            else
            {
                next_move = -1;
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
            StartCoroutine(YMove());
        }
    }
    IEnumerator Attack()
    {
        CancelInvoke("DecideMove");
        next_move = 0;
        isAttacking = true;
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(Cooldown());
    }
    IEnumerator Cooldown()
    {
        if (player.position.x > transform.position.x)
        {
            next_move = 1;
        }
        else
        {
            next_move = -1;
        }

        isCoolDown = true;

        yield return new WaitForSeconds(ATTACK_COOL);
        isCoolDown = false;
    }
    IEnumerator YMove()
    {
        rigid.velocity = new Vector2(next_move, 1);
        yield return new WaitForSeconds(1);
        rigid.velocity = new Vector2(next_move, 0);
    }
    void DecideMove()
    {
        next_move = Random.Range(-1, 2);
        if (!book_anim.GetBool("IsPlayerClosed"))
        {
            Invoke("DecideMove", 4);
        }
    }
}
