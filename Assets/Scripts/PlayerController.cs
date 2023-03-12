using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour {

    private float horizontal;
    private float jump;
    private float speed;
    private float jumpingPower;
    public bool isFacingRight;
    private bool isGrounded;

    private Animator animator;
    private Rigidbody2D rb;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Start() {
        speed = 8f;
        jumpingPower = 20f;
        isFacingRight = true;
        isGrounded = true;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 7f;
    }

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
        jump = Input.GetAxisRaw("Jump");
        animator.SetFloat("xVelocity", Mathf.Abs(horizontal));
        animator.SetFloat("yVelocity", jump);

        FlipWhileWalking();
        Jump();
        Run();
    }

    private void FixedUpdate() {
        GroundCheck();
    }

    /// <summary>
    /// Checks if the gameObject is grounded. If not grounded, sets the Jump bool in the Animator to true.
    /// If grounded, sets the Jump bool in the Animator to false.
    /// </summary>
    private void GroundCheck() {
        isGrounded = false;
        if (Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer) == true) {
            isGrounded = true;
        }
        animator.SetBool("Jump", !isGrounded);
    }

    private void Run() {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    
    private void Jump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            isGrounded = false;
            animator.SetBool("Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }

    public void FlipWhileWalking() {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            GetComponent<SpriteRenderer>().flipX = !isFacingRight;
        }
    }
}
