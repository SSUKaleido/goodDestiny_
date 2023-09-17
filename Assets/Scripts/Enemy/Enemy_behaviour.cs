using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{
    #region Public Variables
    public enum Type { Nomal, Boss };
    public Type enemyType;
    public float attackDistance = 2f; //���� �ּ� �Ÿ�
    public float moveSpeed;
    public float damage;
    public float slashDamage = 10f;
    public float timer; //���� ��Ÿ�� Ÿ�̸�
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //���� �ȿ� �÷��̾ ���Դ��� Ȯ��
    public GameObject hitBox;
    public GameObject hotZone;
    public GameObject triggerArea;
    public GameObject particle;
    public GameObject bullet;
    public int ranAction;
    #endregion

    #region Private Variables
    private Enemy_status status;
    private Animator anim;
    private float distance; //�÷��̾���� �Ÿ�
    private bool attackMode;
    private bool cooling; //���� ��Ÿ�� ������ Ȯ��
    private bool phase2;
    private bool skill_canRange = true;
    private bool skill_canTeleport = true;
    private float intTimer;
    private float atkNum = 0f;
    #endregion

    void Awake()
    {
        SeletTarget();
        intTimer = timer;
        status = GetComponentInChildren<Enemy_status>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!attackMode && !status.isDead)
            Move();

        if (!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !status.isDead)
            SeletTarget();

        if (inRange && !status.isDead)
            EnemyLogic();

        //Boss 2phase
        if (enemyType == Type.Boss && status.curHealth < status.maxHealth / 2f)
        {
            phase2 = true;
            particle.SetActive(true);
            moveSpeed = 7f;
        }
            
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            if (enemyType == Type.Nomal)
                Attack();
            else if (enemyType == Type.Boss)
            {
                BossAtk();
            }
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

        if (timer <= 0 && cooling && attackMode)
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

        if (distanceToLeft > distanceToRight)
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
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

    void BossAtk()
    {
        timer = intTimer; //���� ������ ������ �� timer ����
        attackMode = true;


        if (skill_canTeleport == true)
            BossTeleportAtk();

        if (phase2 && cooling == false && skill_canRange)
            attackDistance = 6f;


        anim.SetBool("canWalk", false);
        anim.SetFloat("Blend", atkNum);
        anim.SetBool("Attack", true);

        atkNum = 0f;
    }

    void BossSlash()
    {
        if (!phase2)
            return;

        GameObject instantBulletA = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D rigidBulletA = instantBulletA.GetComponent<Rigidbody2D>();
        rigidBulletA.velocity = transform.right * 20;
    }

    void BossTeleportAtk()
    {
        cooling = true;
        skill_canTeleport = false;
        Invoke("TeleportCooltime", 40f);

        transform.position = target.position;
        atkNum = 1f;
        BossAtk();
    }

    void RangeCooltime()
    {
        skill_canRange = true;
    }

    void TeleportCooltime()
    {
        skill_canTeleport = true;
    }
}


