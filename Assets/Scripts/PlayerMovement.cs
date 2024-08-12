using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float movement;
    public float speed;
    bool isGrounded;
    public LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    private bool jumpInput;
    [SerializeField] private float jumpForce = 5f;
    Animator anim;
    Animator childAnim;

    public static bool isDed = false;

    [Header("Assist")]
    [SerializeField] private float jumpBufferLength = 0.2f;
    [SerializeField] private float jumpBufferTimer;
    bool jumpBuffer;


    [SerializeField] private float cayoteTimeLength = 0.1f;

    [Header("Gravity")]
    [SerializeField] private float gravityScale = 1f;
    [SerializeField] private float fallMultiplier = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        childAnim = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.6f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        Animation();
        if (isDed == true)
        {
            return;
        }
        Flip();
        if (isGrounded == true)
        {
            cayoteTimeLength = 0.1f;
        }
        else
        {
            cayoteTimeLength -= Time.deltaTime;
        }

        if (rb.velocity.y > 0f)
        {
            cayoteTimeLength = 0f;
        }
        else if (rb.velocity.y < 0f)
        {
            rb.gravityScale = gravityScale * fallMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -15f));
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }

    private void FixedUpdate()
    {
        if (isDed == true)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        // Process jump in the FixedUpdate method
        if (jumpBuffer == true)
        {
            jumpBufferTimer -= Time.deltaTime;
            if (jumpBufferTimer > 0 && (cayoteTimeLength > 0 || (isGrounded && jumpInput)))
            {
                jumpBuffer = false;
                rb.gravityScale = gravityScale;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                // Reset the jump buffer and cayote time states
                jumpInput = false;

            }
            else if (jumpBufferTimer <= 0)
            {
                jumpBuffer = false;
            }
        }

        if (jumpBuffer == false)
        {
            jumpBufferTimer = 0;
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<float>();
    }
    public void Jump(InputAction.CallbackContext ctx)
    {
        print("Jump");
        if (ctx.started)
        {
            jumpBuffer = true;
            jumpBufferTimer = jumpBufferLength;
            jumpInput = true;
        }
        if (ctx.canceled)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
    }
    void Flip()
    {
        if (movement > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (movement < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    void Animation()
    {
        anim.SetBool("isJump", !isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isMove", movement != 0f);
        anim.SetBool("isDed", isDed);
        childAnim.SetBool("isJump", !isGrounded);
        childAnim.SetFloat("yVelocity", rb.velocity.y);
        childAnim.SetBool("isMove", movement != 0f);
        childAnim.SetBool("isDed", isDed);
    }   
}
