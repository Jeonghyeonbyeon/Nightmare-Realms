using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private PlayerController player;
    private float originalGravityScale = 1f;
    private bool isDashing = false;
    private float dashTimeLeft;
    private float dashCooldownLeft;
    private Rigidbody2D rb;
    private Vector2 dashDirection;

    void Start()
    {
        player = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownLeft <= 0)
        {
            StartDash();
        }

        if (isDashing)
        { 
            Dash();
        }
        else
        {
            if (dashCooldownLeft > 0)
            {
                dashCooldownLeft -= Time.deltaTime;
            }
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        dashCooldownLeft = dashCooldown;
        dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.gravityScale = 0;
        GetComponent<GhostEffect>().enabled = true;
    }

    void Dash()
    {
        if (dashTimeLeft > 0)
        {
            player.isDash = true;
            rb.velocity = dashDirection * dashSpeed;
            rb.AddForce(dashDirection * dashSpeed, ForceMode2D.Impulse);
            dashTimeLeft -= Time.deltaTime;
        }
        else
        {
            isDashing = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = originalGravityScale;
            GetComponent<GhostEffect>().enabled = false;
            player.isDash = false;
        }
    }
}
