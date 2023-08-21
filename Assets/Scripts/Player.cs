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
    //public GameObject[] weapons;
    //public bool[] hasWeapons;
    public int swordDamage = 10;

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
    //bool isFireReady = true;
    bool isDead;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    float curTime;
    public float coolTime = 0.5f;
    public Transform pos;
    public Vector2 boxSize;

    //public Weapon equipWeapon;
    //float fireDelay;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        //hasWeapons[0] = true;
    }

    void Update()
    {
        GetInput();
        Move();
        Jump();
        Dash();
        Attack();
        //Swap();
    }

    void GetInput()
    { //��ȭ ���� �� �̵� �Ұ� ����
        hAxis = Input.GetAxisRaw("Horizontal");
        hDown = Input.GetButton("Horizontal");
        jDown = Input.GetButtonDown("Jump");
        aDown = Input.GetKeyDown(KeyCode.Z);
        dDown = Input.GetKeyDown(KeyCode.C);
        sDown1 = Input.GetKeyDown(KeyCode.Alpha1);
        sDown1 = Input.GetKeyDown(KeyCode.Alpha2);

    }

    void Move()
    {
        //Move Speed
        rigid.velocity = new Vector2(hAxis * speed, rigid.velocity.y);

        anim.SetBool("isWalking", hDown);

        //Stop Speed
        if (Input.GetButtonUp("Horizontal")) {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            anim.SetBool("isWalking", false);
        }

        //Direction Sprite(���� ��ȯ)
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
        if(curTime <= 0)
        {
            if(dDown)
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if(collider.tag == "Enemy")
                    {
                        collider.GetComponent<PhysicBookHit>().OnDamage(swordDamage);
                    }
                }

                anim.SetTrigger("atk");
                curTime = coolTime;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    //�Ʒ��� ������ �ƴ� ���븦 ���� �÷��̾��� �� ����.
    /*void Attack()
    {
            if (equipWeapon == null || isDead)
                return;

            fireDelay += Time.deltaTime;
            isFireReady = equipWeapon.rate < fireDelay;

            if (dDown && isFireReady)
            {
                equipWeapon.Use(); //���� ������ ������Լ� ����
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
            if (weapons[weaponIndex].activeSelf == true) //������ �����ư Ŭ���� ����
                return;
            if (equipWeapon != null) //����� �� ����(��������)
                equipWeapon.gameObject.SetActive(false);

            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);

            anim.SetTrigger("doSwap");
        }
    }*/

    void FixedUpdate()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //��������: õ�忡 �ε��ĵ� ground��� �ν� > tile�� ���� Ȥ�� ray ������� ��ü?
        if (collision.gameObject.tag == "Ground") {
            anim.SetBool("isJumping", false);
            isJump = false;
            isDoubleJump = false;
            Invoke("DashDelay", dashDelaySec);
        }
    }
}