using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public bool FacingLeft => facingLeft;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashMultiplier = 4f;
    [SerializeField] private TrailRenderer trailRenderer;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Knockback knockback;
    private PlayerInput playerInput;

    private bool facingLeft;
    private bool isDashing;
    private float originalMoveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        playerInput = GetComponent<PlayerInput>();

        originalMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        AdjustFacingDirection();

        if (playerInput.DashPressed)
        {
            Dash();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (knockback != null && knockback.GettingKnockedBack)
            return;

        rb.MovePosition(
            rb.position +
            playerInput.Movement *
            moveSpeed *
            Time.fixedDeltaTime);
    }

    private void AdjustFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint =
            Camera.main.WorldToScreenPoint(transform.position);

        facingLeft = mousePos.x < playerScreenPoint.x;
        spriteRenderer.flipX = facingLeft;
    }

    private void Dash()
    {
        if (isDashing)
            return;

        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        isDashing = true;

        moveSpeed *= dashMultiplier;

        if (trailRenderer != null)
            trailRenderer.emitting = true;

        yield return new WaitForSeconds(0.2f);

        moveSpeed = originalMoveSpeed;

        if (trailRenderer != null)
            trailRenderer.emitting = false;

        yield return new WaitForSeconds(0.25f);

        isDashing = false;
    }
}