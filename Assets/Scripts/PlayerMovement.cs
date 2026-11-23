using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Jump settings")]
    public float jumpForce = 12f;
    public float gravity = -30f;

    [Header("Ground detect")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;

    [Header("Animation")]
    public Animator animator;

    private Rigidbody2D rb;

    private bool isGrounded;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("Player anim is missing");
        }

        if (groundCheck == null)
        {
            Debug.LogError("Groundcheck is not assigned");
        }
    }

    void Update()
    {
        CheckGround();

        //only allow jumping when player is touching the ground
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            Jump();
        }

        //update animation states
        UpdateAnimation();
    }

    void Jump()
    {
        //apply upward force
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        isJumping = true;

        //switch to jumping animation
        animator.SetBool("IsJumping", true);
    }

    void CheckGround()
    {
        //check for ground underneath the player
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //reset jumping state once the player lands
        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            isJumping = false;
        }
    }

    void UpdateAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("IsJumping", isJumping);
        }
    }

    //visually show ground detection area in the scene
    void OnDrawGizmosSelected()
    {
        //check if groundchedk exists
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}