using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private PlayerController playerController;
    private SpriteRenderer sprite;
    private Player player;
    private Rigidbody2D rb;
    private Vector2 dashDirection;
    private float originalGravityScale = 1f;
    private bool isDashing = false;
    private float dashTimeLeft;
    private float dashCooldownLeft;
    private bool isCoolTimeDash;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        sprite = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;
        isCoolTimeDash = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && dashCooldownLeft <= 0 && player.curMana >= 5 && isCoolTimeDash)
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
        player.enabled = false;
        StartCoroutine(CoolTimeFunc(0.75f, 0.75f));
        AudioClip clip = SoundManager.instance.jump;
        SoundManager.instance.PlaySFX_1(clip);
        isCoolTimeDash = false;
        player.curMana -= 5;
        isDashing = true;
        dashTimeLeft = dashTime;
        dashCooldownLeft = dashCooldown;
        dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.gravityScale = 0;
        GetComponent<GhostEffect>().enabled = true;
        UIManager.instance.SetPlayerMana(player.curMana, player.maxMana);
    }

    void Dash()
    {
        if (dashTimeLeft > 0)
        {
            playerController.isDash = true;
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
            player.enabled = true;
            playerController.isDash = false;
        }
    }

    IEnumerator CoolTimeFunc(float cooltime, float cooltimeMax)
    {
        UIManager.instance.dashCoolText.gameObject.SetActive(true);

        while (cooltime > 0.0f)
        {
            if (cooltime <= 0.1f)
            {
                cooltime = 0;
                UIManager.instance.dashCoolText.gameObject.SetActive(false);
                isCoolTimeDash = true;
            }
            else cooltime -= Time.deltaTime;

            UIManager.instance.dashIcon.fillAmount = cooltime / cooltimeMax;
            UIManager.instance.dashCoolText.text = cooltime.ToString();
            yield return new WaitForFixedUpdate();
        }
    }
}
