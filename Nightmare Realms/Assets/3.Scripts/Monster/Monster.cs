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
    protected Transform playerTransform;
    protected SpriteRenderer sprite;
    protected Animator anim;
    protected Rigidbody2D rigid;
    protected Slider hp;
    public MonsterData monsterData;
    public int maxHP;
    public int curHP;
    protected float detectionRange;
    protected float attackRange;
    protected float speed;

    protected virtual void Start()
    {
        maxHP = monsterData.maxHP;
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

        AudioClip clip = SoundManager.instance.swordHit;
        SoundManager.instance.PlaySFX_2(clip);

        curHP -= damage;
        SetHPSliderValue();

        if (sprite != null)
            sprite.color = Color.red;
        else
            Debug.LogWarning("SpriteRenderer is null or destroyed");
        yield return new WaitForSecondsRealtime(0.15f);
        if (sprite != null)
            sprite.color = new Color(150 / 255f, 150 / 255f, 150 / 255f);
        else
            Debug.LogWarning("SpriteRenderer is null or destroyed");

        if (curHP <= 0)
        {
            ChangeStateTo(State.Dead);
        }
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