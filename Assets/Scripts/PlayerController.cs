using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour {

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 28f;
    private bool isFacingRight = true;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));


        IsJumping();
        IsRunning();
        Flip();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void IsRunning() {

        Debug.Log("Gravity scale trying to set to 10");
        if((Input.GetKeyDown("left") || Input.GetKeyDown("right")) && IsGrounded()) {
            rb.gravityScale = 10f;
        }
    }

    private void IsJumping() {
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            rb.gravityScale = 4f;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        //this allows us to jump higher by pressing the jump button longer, and jump lower by just tapping the jump button
        if(Input.GetButtonDown("Jump") && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            animator.SetBool("Jumping", true);
        }
    }

    private void Flip() {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            //transform.parent.localScale = localScale;
        }
    }
}
