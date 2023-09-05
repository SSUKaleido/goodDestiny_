using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    #region Public Variables
    public float attackDistance; //���� �ּ� �Ÿ�
    public float moveSpeed;
    public float timer; //���� ��Ÿ�� Ÿ�̸�
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //���� �ȿ� �÷��̾ ���Դ��� Ȯ��
    public GameObject hitBox;
    public GameObject hotZone;
    public GameObject triggerArea;
    #endregion

    #region Private Variables
    private Animator anim;
    private float distance; //�÷��̾���� �Ÿ�
    private bool attackMode;
    private bool cooling; //���� ��Ÿ�� ������ Ȯ��
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
        timer = intTimer; //���� ������ ������ �� timer ����
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
            //timer = intTimer; ������ �����ϰԵǸ� ����� �ڵ� 
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    //�ִϸ��̼� �����ϴ� �߿� ����Ǵ� �Լ�(����Ƽ������ ����)
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
