using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
<<<<<<< Updated upstream
    public GameManager manager;
    public Camera followCamera;

    public float speed = 8;
    public float dashSpeed = 30;
    float defaultSpeed;
    public float dashDelaySec = 1.5f;
    public float jumpPower = 15;
=======
    #region Singleton
    public static Player instance;
    #endregion
    public float speed = 15;
    public float jumpPower = 15;
    public float dashPower = 150;
    public float dashDelaySec = 0.5f;
    //public GameObject[] weapons;
    //public bool[] hasWeapons;
    public float swordDamage = 10;
>>>>>>> Stashed changes

    public int coin;
    public int health = 100;
    public int score;

    public int maxCoin = 10000;
    public int maxHealth = 100;

    float hAxis;

    bool hDown;
    bool jDown;
    bool zDown;
    bool xDown;
    bool cDown;

    bool isJump;
    bool isDoubleJump;
    bool isDash;
    bool isDamage;
    bool isDead;

    Rigidbody2D rigid;
    Animator anim;
    MeshRenderer mesh;

    public int swordDamage = 10;
    public int atkNum = 0;
    float swordCurTime;
    public float swordCoolTime = 0.3f;
    public Transform swordRange;
    public Vector2 boxSize;

    float bulletCurTime;
    public float bulletCoolTime = 2f;
    public GameObject bullet;
    public Transform bulletPos;


    void Awake()
    {
        instance = this;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mesh = GetComponent<MeshRenderer>();

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
    }

    void GetInput()
    { //대화 실행 중 이동 불가 설정
        hAxis = Input.GetAxisRaw("Horizontal");
        hDown = Input.GetButton("Horizontal");
        jDown = Input.GetButtonDown("Jump");
        zDown = Input.GetKeyDown(KeyCode.Z);
        xDown = Input.GetKeyDown(KeyCode.X);
        cDown = Input.GetKeyDown(KeyCode.C);
    }

    void Move()
    {
        //Move Speed
        rigid.velocity = new Vector2(hAxis * defaultSpeed, rigid.velocity.y);

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
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(swordRange.position, boxSize, 0);
                
                PlayAtkAnimation(atkNum++);
                if (atkNum > 1)
                    atkNum = 0;

                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Enemy")
                        collider.GetComponent<MagicBook>().cur_health -= swordDamage; 
                }

                swordCurTime = swordCoolTime;
            }
        }
        else
        {
            swordCurTime -= Time.deltaTime;
        }
    }

    void PlayAtkAnimation(int atkNum)
    {
        anim.SetFloat("Blend", atkNum);
        anim.SetTrigger("atk");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(swordRange.position, boxSize);
    }

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

    void OnTriggerEnter(Collider other)
    {
       if (other.tag == "EnemyBullet")
        {
            if (!isDamage)
            {
                //스크립트 이름을 Bullet으로 수정하면 좋을 듯..
                //MagicHit enemyBullet = other.GetComponent<MagicHit>();
                //health -= enemyBullet.damage;

                StartCoroutine(OnDamage());
            }

            if (other.GetComponent<Rigidbody>() != null)
                Destroy(other.gameObject);
        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        mesh.material.color = Color.blue;
        if (health <= 0 && !isDead)
            OnDie();

        yield return new WaitForSeconds(1f);

        isDamage = false;
        mesh.material.color = Color.white;
    }

    void OnDie()
    {
        anim.SetTrigger("doDie");
        isDead = true;
        //manager.PlayerDead();
    }
}