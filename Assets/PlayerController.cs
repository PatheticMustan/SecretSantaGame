using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float horizontalSpeed;
    public float jumpMultiplier;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    public Vector2 absoluteMaxSpeed;

    public bool isGroundedDefinitely;

    public float jumpCooldown;
    public float currentJumpCooldown;

    private Rigidbody2D rb;
    private Vector2 controls;
    private Vector3 colliderOffset;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        colliderOffset = GetComponent<BoxCollider2D>().offset;
    }

    void Update() {
        controls = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (currentJumpCooldown > 0) currentJumpCooldown -= Time.deltaTime;
    }

    void FixedUpdate() {
        rb.AddForce(Vector2.right * (controls.x) * horizontalSpeed);

        if (controls.y > 0.5f && isGrounded() && currentJumpCooldown <= 0) {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(0, rb.velocity.y));
            rb.AddForce(Vector2.up * (controls.y) * jumpMultiplier);

            currentJumpCooldown = jumpCooldown;
            Debug.Log(rb.totalForce);
        }

        isGroundedDefinitely = isGrounded();
    }

    private void LateUpdate() {
        // clip velocity
        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x, -absoluteMaxSpeed.x, absoluteMaxSpeed.x),
            Mathf.Clamp(rb.velocity.y, -absoluteMaxSpeed.y, absoluteMaxSpeed.y)
        );
        //Debug.Log(rb.velocity);
    }

    public bool isGrounded() {
        return Physics2D.BoxCast(
            transform.position + colliderOffset,
            boxSize,
            0,
            -transform.up,
            castDistance,
            groundLayer
        );
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(
            transform.position + colliderOffset - transform.up * castDistance,
            boxSize
        );
    }

    public Vector2 GetControls() {
        return controls;
    }
}