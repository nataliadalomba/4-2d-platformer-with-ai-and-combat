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
        jumpingPower = 20f;
        isFacingRight = true;
        isGrounded = true;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("xVelocity", Mathf.Abs(horizontal));

        Flip();
        Jump();
        Run();
    }

    private void FixedUpdate() {
        //trying to have the run animation play while on slopes
        if (Jump()) {
            animator.SetFloat("yVelocity", rb.velocity.y);
        } else {
            animator.SetFloat("xVelocity", rb.velocity.x);
        }
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
        //if((horizontal != 0) && isGrounded) {
         //   rb.gravityScale = 20f;
        //}
    }
    
    private bool Jump() {
        bool isJumping = false;
        if (Input.GetButtonDown("Jump") && isGrounded) {
            isGrounded = false;
            rb.gravityScale = 7f;
            animator.SetBool("Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isJumping = true;
            return isJumping;

            //this allows us to jump higher by pressing the jump button longer, and jump lower by just tapping the jump button
            //if(Input.GetButtonDown("Jump") && rb.velocity.y > 0f) {
            //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            //    animator.SetBool("Jump", true);
            //}
        }
        return isJumping;
    }

    private void Flip() {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            GetComponent<SpriteRenderer>().flipX = !isFacingRight;
        }
    }
}
