using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    Idle, 
    Chase,
    Attack,
    Dead 
}

public abstract class Monster : MonoBehaviour
{
    protected State currentState;
    public LayerMask layer;
    public int maxHP;
    protected Transform playerTransform;
    protected SpriteRenderer sprite;
    protected Animator anim;
    protected Rigidbody2D rigid;
    protected Slider hp;
    protected int curHP;
    protected float detectionRange;
    protected float attackRange;
    protected float speed;

    protected virtual void Start()
    {
        curHP = maxHP;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        hp = transform.Find("Canvas").GetChild(0).GetComponent<Slider>();
        SetHPSliderValue();
        ChangeStateTo(State.Idle);
    }

    protected virtual void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Dead:
                Dead();
                break;
        }
    }

    public IEnumerator TakeDamage(int damage)
    {
        if (!transform.Find("Canvas").gameObject.activeSelf)
            transform.Find("Canvas").gameObject.SetActive(true);

        curHP -= damage;
        SetHPSliderValue();

        if (curHP <= 0)
        {
            ChangeStateTo(State.Dead);
        }
        Color color = Color.red;
        sprite.color = color;
        yield return new WaitForSecondsRealtime(0.15f);
        color = Color.white;
        sprite.color = color;
    }

    protected void SetHPSliderValue() => hp.value = (float)curHP / maxHP;

    protected abstract void Idle();
    protected abstract void Chase();
    protected abstract void Attack();
    protected abstract void Dead();

    protected void ChangeStateTo(State newState)
    {
        currentState = newState;
    }
}