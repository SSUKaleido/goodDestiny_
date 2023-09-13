using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.XR;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static Player instance;
    public GameManager manager;
    public Camera followCamera;

    public float damage = 10;   //근접, 원거리 공격 모두 포함(원거리 공격은 5초후에 사라지기 때문에 다중 공격가능)
    public float speed = 8;
    public float dashSpeed = 30;
    float defaultSpeed;
    public float dashDelaySec = 1.5f;
    public float jumpPower = 15;

    public int score;

    public float hAxis;

    bool hDown;
    bool jDown;
    bool zDown;
    bool xDown;
    bool cDown;

    bool isJump;
    bool isDoubleJump;
    bool isDash;
    bool isBorder;
    public bool isDamage;

    public AudioManager am;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer mesh;

    public int atkNum = 0;
    float swordCurTime;
    public float swordCoolTime = 0.3f;
    public GameObject swordRange;
    public Vector2 boxSize;

    float bulletCurTime;
    public float bulletCoolTime = 2f;
    public GameObject bullet;
    public Transform bulletPos;


    void Awake()
    {
        instance =this;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mesh = GetComponent<SpriteRenderer>();

        defaultSpeed = speed;
        //hasWeapons[0] = true;
    }

    void Update()
    {
        GetInput();
        Jump();
        Dash();
        Attack();
        BulletFire();
    }

    void FixedUpdate()
    {
        Move();
        StopToWall();
    }

    void GetInput()
    { //대화 실행 중 이동 불가 설정
        hAxis = StoryManager.instance.isStory ? 0 : Input.GetAxisRaw("Horizontal");
        hDown = StoryManager.instance.isStory ? false : Input.GetButton("Horizontal");
        jDown = StoryManager.instance.isStory ? false : Input.GetButtonDown("Jump");
        zDown = StoryManager.instance.isStory ? false : Input.GetKeyDown(KeyCode.Z);
        xDown = StoryManager.instance.isStory ? false : Input.GetKeyDown(KeyCode.X);
        cDown = StoryManager.instance.isStory ? false : Input.GetKeyDown(KeyCode.C);
    }

    void Move()
    {
        //Move Speed
        if(!isBorder)
            rigid.velocity = new Vector2(hAxis * defaultSpeed, rigid.velocity.y);

        if (hAxis!=0 && !isJump)
            am.PlaySFX("Run");
        anim.SetBool("isWalking", hDown);

        //Stop Speed
        if (Input.GetButtonUp("Horizontal")) {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            anim.SetBool("isWalking", false);
        }

        //Direction Sprite(방향 전환)
        if (hAxis == -1)
            transform.rotation = Quaternion.Euler(0,180,0);
        else if(hAxis == 1)
            transform.rotation = Quaternion.Euler(0,0,0);
    }

    void Jump()
    {
        if (jDown && isJump && !isDoubleJump){
            isDoubleJump = true;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        if (jDown && !isJump && !isDoubleJump){
            isJump = true;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
    }

    void Dash()
    {
        if (zDown && hDown && !isDash)
        {
            isDash = true;
            Vector2 vec = new Vector2(rigid.velocity.x, 0);
            rigid.AddForce(vec, ForceMode2D.Impulse);
            am.PlaySFX("Dash");
            anim.SetBool("isDashing", true);
            defaultSpeed = dashSpeed;
            Invoke("DashExit", 0.15f);

            if (!isJump)
                Invoke("DashDelay", dashDelaySec);
        }
    }

    void DashExit()
    {
        defaultSpeed = speed;
        anim.SetBool("isDashing", false);
    }

    void DashDelay()
    {
         isDash = false;
    }

    void BulletFire()
    {
        if(bulletCurTime <= 0)
        {
            if(xDown)
            {
                Instantiate(bullet, bulletPos.position, transform.rotation);
                am.PlaySFX("Skill");
                bulletCurTime = bulletCoolTime;
            }
        }
        bulletCurTime -= Time.deltaTime;
    }    

    void Attack()
    {
        if(swordCurTime <= 0)
        { 
            if(cDown)
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(swordRange.transform.position, boxSize, 0);
                if (atkNum == 0)
                    am.PlaySFX("Attack1");
                else
                    am.PlaySFX("Attack2");
                PlayAtkAnimation(atkNum++);
                if (atkNum > 1)
                    atkNum = 0;

                swordCurTime = swordCoolTime;
            }
        }
        else
        {
            swordCurTime -= Time.deltaTime;
        }
    }

    //애니메이션 공격하는 중에 실행되는 함수(유니티내에서 편집)
    void SwordRangeEnabled()
    {
        swordRange.SetActive(true);
    }
    void SwordRangeDisabled()
    {
        swordRange.SetActive(false);
    }

    void PlayAtkAnimation(int atkNum)
    {
        anim.SetFloat("Blend", atkNum);
        anim.SetTrigger("atk");
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(swordRange.transform.position, boxSize);
    }
    */
    void OnCollisionEnter2D(Collision2D collision)
    {
        //버그있음: 천장에 부딪쳐도 ground라고 인식 > tile맵 변경 혹은 ray 사용으로 대체?
        if (collision.gameObject.tag == "Ground") {
            anim.SetBool("isJumping", false);
            isJump = false;
            isDoubleJump = false;
            Invoke("DashDelay", dashDelaySec);
        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        mesh.material.color = Color.red;
        yield return new WaitForSeconds(1f);

        isDamage = false;
        mesh.material.color = Color.white;
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.right * 1, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.right, 1, LayerMask.GetMask("Wall"));
    }
}