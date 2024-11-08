using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float dashForce = 20f;
    public float dashCooldown = 2f;
    private Rigidbody rb;
    public Transform planetCenter;
    public float gravityForce = 9.81f;
    private bool canDash = true;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;
    private bool isGrounded;
    private int jumpsRemaining = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Dash") && canDash)
        {
            Dash();
        }

        if (isGrounded)
        {
            jumpsRemaining = 2;
        }

        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpsRemaining--;
        }
    }

    void FixedUpdate()
    {
        Vector3 gravityDirection = (planetCenter.position - transform.position).normalized;
        rb.AddForce(gravityDirection * gravityForce);

        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 0.5f, 0), groundCheckDistance, groundLayer);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    void Dash()
    {
        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
        canDash = false;
        StartCoroutine(DashCooldown());
    }

    System.Collections.IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
