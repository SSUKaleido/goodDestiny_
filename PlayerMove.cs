using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    bool doubleJumpState = false;
    bool isGround = false;

    public float speed;

    private float default_speed;
    public float dash_speed;
    public float default_time;
    private float dash_time;
    private bool isdash;

    
    void Awake()
    {
        default_speed = speed;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        //더블점프 구현
        DoubleJump();

        Dash();

        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //Direction Sprite
        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }
    void DoubleJump()
    {
        if (rigid.velocity.y == 0)
            isGround = true;
        else
            isGround = false;
        if (isGround)
            doubleJumpState = true;
        if (isGround && Input.GetButton("Jump"))
        {
            JumpAddForce();
        }
        else if (doubleJumpState && Input.GetButtonDown("Jump"))
        {
            JumpAddForce();
            doubleJumpState = false;
        }
        }
    void JumpAddForce()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, 0f);
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        anim.SetBool("isJumping", true);
    }

    void Dash()
    {
        float h = Input.GetAxis("Horizontal");
        rigid.velocity = new Vector2(h * default_speed, rigid.velocity.y);
        if (Input.GetKeyDown(KeyCode.X))
        {
            isdash = true;
        }
        if (dash_time <= 0)
        {
            rigid.velocity = new Vector2(h * speed, rigid.velocity.y);
            if (isdash)
            {
                dash_time = default_time;
            }
        }
        else
        {
            dash_time -= Time.deltaTime;
            rigid.velocity = new Vector2(h * dash_speed, rigid.velocity.y);
        }
        isdash = false;

    }

    void FixedUpdate()
        {


        //Landing Platform
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                
                    if (rayHit.distance < 0.4f)
                        anim.SetBool("isJumping", false);
               
            }
        }

        }
    }