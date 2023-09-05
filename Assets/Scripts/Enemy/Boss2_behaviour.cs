using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    #region Public Variables
    public float attackDistance; //공격 최소 거리
    public float moveSpeed;
    public float timer; //공격 쿨타임 타이머
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //범위 안에 플레이어가 들어왔는지 확인
    public GameObject hitBox;
    public GameObject hotZone;
    public GameObject triggerArea;
    #endregion

    #region Private Variables
    private Animator anim;
    private float distance; //플레이어와의 거리
    private bool attackMode;
    private bool cooling; //공격 쿨타임 중인지 확인
    private float intTimer;
    #endregion

    void Awake()
    {
        SeletTarget();
        intTimer = timer;
        anim = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (!attackMode && !Enemy_status.instance.isDead)
            Move();

        if(!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !Enemy_status.instance.isDead)
            SeletTarget();

        if(inRange && !Enemy_status.instance.isDead)
            EnemyLogic();
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if(distance > attackDistance)
        {
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
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

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
            //timer = intTimer; 어차피 공격하게되면 실행될 코드 
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    //애니메이션 공격하는 중에 실행되는 함수(유니티내에서 편집)
    void TriggerCooling()
    {
        cooling = true;
    }
    void HitBoxEnabled()
    {
        hitBox.SetActive(true);
    }
    void HitBoxDisabled()
    {
        hitBox.SetActive(false);
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SeletTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if(distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }
}
