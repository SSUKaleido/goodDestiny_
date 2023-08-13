using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 15;
    public float jumpPower = 15;
    public float dashPower = 100;
    public float dashDelaySec = 0.5f;

    float hAxis;

    bool hDown;
    bool jDown;
    bool xDown;

    public bool isJump;
    public bool isDoubleJump;
    public bool isDash;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        GetInput();
        Move();
        Jump();
        Dash();

    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        hDown = Input.GetButton("Horizontal");
        jDown = Input.GetButtonDown("Jump");
        xDown = Input.GetKeyDown(KeyCode.X);
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
        if(xDown && hDown && !isDash){
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

    void FixedUpdate()
    {

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