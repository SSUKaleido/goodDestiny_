using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 15;
    public float jumpPower = 15;
    public float dashPower = 150;
    public float dashDelaySec = 0.5f;
    public GameObject[] weapons;
    public bool[] hasWeapons;

    public int coin;
    public int health = 100;
    public int score;

    public int maxCoin = 10000;
    public int maxHealth = 100;

    float hAxis;

    bool hDown;
    bool jDown;
    bool aDown;
    bool dDown;
    bool sDown1;
    bool sDown2;

    bool isJump;
    bool isDoubleJump;
    bool isDash;
    bool isFireReady = true;
    bool isBorder;
    bool isDead;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;



    public Weapon equipWeapon;
    float fireDelay;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        hasWeapons[0] = true;
    }

    void Update()
    {
        GetInput();
        Move();
        Jump();
        Dash();
        Attack();
        Swap();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        hDown = Input.GetButton("Horizontal");
        jDown = Input.GetButtonDown("Jump");
        aDown = Input.GetKeyDown(KeyCode.A);
        dDown = Input.GetKeyDown(KeyCode.D);
        sDown1 = Input.GetKeyDown(KeyCode.Alpha1);
        sDown1 = Input.GetKeyDown(KeyCode.Alpha2);

    }

    void Move()
    {
        //Move Speed
        if(!isBorder)
            rigid.velocity = new Vector2(hAxis * speed, rigid.velocity.y);

        anim.SetBool("isWalking", hDown);

        //Stop Speed
        if (Input.GetButtonUp("Horizontal")) {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            anim.SetBool("isWalking", false);
        }

        //Direction Sprite(방향 전환)
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
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
        if(aDown && hDown && !isDash){
            isDash = true;
            rigid.velocity = new Vector2(hAxis * dashPower, rigid.velocity.y);
            
            if(!isJump)
               Invoke("DashDelay", dashDelaySec);
        }
    }

    void DashDelay()
    {
         isDash = false;
    }

    void Attack()
    {
        if (equipWeapon == null || isDead)
            return;

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if (dDown && isFireReady)
        {
            equipWeapon.Use(); //조건 충족후 무기속함수 실행
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Sword ? "doSwing" : "doShot");
            fireDelay = 0;
        }
    }

    void Swap()
    {
        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;

        if ((sDown1 || sDown2) && hasWeapons[weaponIndex] && !isDead)
        {
            if (weapons[weaponIndex].activeSelf == true) //장착된 무기버튼 클릭시 조건
                return;
            if (equipWeapon != null) //빈손일 때 조건(에러방지)
                equipWeapon.gameObject.SetActive(false);

            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);

            anim.SetTrigger("doSwap");
        }
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }

    void FixedUpdate()
    {
        StopToWall();
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
}