using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float moveSpeed = 6f;

    [Header("Jump")]
    [SerializeField] KeyCode jumpKey = KeyCode.W;
    [SerializeField] float jumpForce = 12f;
    [SerializeField] float coyoteTime = 0.1f;
    [SerializeField] float jumpBuffer = 0.1f;

    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 groundCheckSize = new Vector2(0.6f, 0.12f);
    [SerializeField] LayerMask groundMask;

    Rigidbody2D rb;
    Animator anim;

    float moveX;
    bool grounded;
    float coyoteTimer;
    float bufferTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // A/D or arrows
        moveX = Input.GetAxisRaw("Horizontal");

        // Raw contact check
        bool touchingGround = groundCheck != null &&
            Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundMask);

        // IMPORTANT: ignore ground while moving upward (prevents "landing" when jumping through one-way platforms)
        grounded = touchingGround && rb.linearVelocity.y <= 0.01f;

        // Timers
        coyoteTimer = grounded ? coyoteTime : Mathf.Max(0f, coyoteTimer - Time.deltaTime);

        if (Input.GetKeyDown(jumpKey))
            bufferTimer = jumpBuffer;
        else
            bufferTimer = Mathf.Max(0f, bufferTimer - Time.deltaTime);

        // Jump
        if (bufferTimer > 0f && coyoteTimer > 0f)
        {
            bufferTimer = 0f;
            coyoteTimer = 0f;

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            anim.SetTrigger("Jump");
        }

        // Animator
        anim.SetBool("isMoving", Mathf.Abs(moveX) > 0.01f);
        anim.SetBool("Grounded", grounded);

        // Flip
        if (Mathf.Abs(moveX) > 0.01f)
        {
            Vector3 s = transform.localScale;
            s.x = Mathf.Abs(s.x) * Mathf.Sign(moveX);
            transform.localScale = s;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
    }

    void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
}
