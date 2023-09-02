using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance; //공격 최소 거리
    public float moveSpeed;
    public float timer; //공격 쿨타임 타이머
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance; //플레이어와의 거리
    private bool attackMode;
    private bool inRange; //범위 안에 플레이어가 들어왔는지 확인
    private bool cooling; //공격 쿨타임 중인지 확인
    private float intTimer;
    #endregion

    void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (inRange) {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, rayCastMask);
            RaycastDebugger();
        }

        if(inRange == false)
        {
            anim.SetBool("canWalk", false);
            StopAttack();
        }

        //플레이어 감지될 때
        if(hit.collider != null)
        {
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            inRange = false;
        }

    }

    //OnTriggerEnter2D를 사용하면 점프해서 나타났을 때 인지 못하는 버그 발생
    private void OnTriggerStay2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;
            inRange = true;
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if(distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if(attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true); 

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer; //공격 범위에 들어왔을 때 timer 리셋
        attackMode = true;

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            //timer = intTimer; 어차피 Attack()이 실행되면 실행될 코드인데.
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);

    }

    void RaycastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);
        }
        else if(attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
    }

    void TriggerCooling()
    {
        cooling = true;
    }
}
