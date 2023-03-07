using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour {

    private float horizontal;
    private float speed;
    private float jumpingPower;
    private bool isFacingRight;
    private bool isGrounded;

    private Animator animator;
    private Rigidbody2D rb;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Start() {
        speed = 8f;
        jumpingPower = 28f;
        isFacingRight = true;
        isGrounded = true;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("xVelocity", Mathf.Abs(horizontal));

        IsJumping();
        Flip();
    }

    private void FixedUpdate() {
        GroundCheck();
        IsRunning();
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    [Tooltip("This is the GroundCheck method.")]
    private void GroundCheck() {
        isGrounded = false;
        if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer) == true) {
            isGrounded = true;
        }
        animator.SetBool("Jump", !isGrounded);
    }

    private void IsRunning() {

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if((Input.GetKeyDown("left") || Input.GetKeyDown("right")) && isGrounded) {
            //animator.SetBool("Jump", false);
            rb.gravityScale = 10f;
        }
    }

    private void IsJumping() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            isGrounded = false;
            animator.SetBool("Jump", true);
            rb.gravityScale = 4f;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        //this allows us to jump higher by pressing the jump button longer, and jump lower by just tapping the jump button
        if(Input.GetButtonDown("Jump") && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            animator.SetBool("Jump", true);
        }
    }

    private void Flip() {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            GetComponent<SpriteRenderer>().flipX = !isFacingRight;
        }
    }
}
