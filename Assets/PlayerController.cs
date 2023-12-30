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

    private Rigidbody2D rb;
    private Vector2 controls;
    private Vector3 colliderOffset;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        colliderOffset = GetComponent<BoxCollider2D>().offset;
    }

    void Update() {
        controls = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Debug.Log(isGrounded());
    }

    void FixedUpdate() {
        rb.AddForce(Vector2.right * (controls.x) * horizontalSpeed);

        if (controls.y > 0.5f && isGrounded()) {
            rb.AddForce(Vector2.up * (controls.y) * jumpMultiplier);
            
        }
    }

    public bool isGrounded() {
        return Physics2D.BoxCast(
            transform.position + colliderOffset - transform.up * castDistance,
            boxSize,
            0,
            -transform.up,
            castDistance,
            groundLayer
        );
    }

    private void OnDrawGizmos() {
        colliderOffset = GetComponent<BoxCollider2D>().offset;
        Gizmos.DrawWireCube(
            transform.position + colliderOffset - transform.up * castDistance,
            boxSize
        );
    }
}