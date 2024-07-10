using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask layer;
    private Rigidbody2D rigid;
    public SpriteRenderer sprite;
    private Animator anim;
    public bool isDash;
    public bool isAttack;
    public bool isDead;
    public bool isSkill;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        rigid.velocity = new Vector2(x * speed, rigid.velocity.y);

        bool hit = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - 0.75f), new Vector2(0.6f, 1), 1, layer);

        sprite.flipX = x != 0 ? x < 0 : sprite.flipX;

        Jump(hit);

        if (isDead) anim.Play("Dead");
        else if (isSkill) anim.Play("Skill_1");
        else if (isAttack) anim.Play("Attack");
        else if (isDash) anim.Play("Dash");
        else if (!hit) anim.Play("Jump");
        else if (x != 0) anim.Play("Move");
        else anim.Play("Idle");
    }

    void Jump(bool hit)
    {
        if (Input.GetKeyDown(KeyCode.Space) && hit)
            rigid.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - 0.75f), new Vector2(0.6f, 1));
    }
}