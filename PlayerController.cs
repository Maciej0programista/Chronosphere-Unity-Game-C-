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
    }

    void FixedUpdate()
    {
        // Grawitacja sferyczna
        Vector3 gravityDirection = (planetCenter.position - transform.position).normalized;
        rb.AddForce(gravityDirection * gravityForce);

        // Sprawdzenie, czy gracz jest na ziemi
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 0.5f, 0), groundCheckDistance, groundLayer);

        // Ruch
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Skok
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
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
