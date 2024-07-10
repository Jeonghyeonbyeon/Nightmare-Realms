using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Slot[] slot;
    [SerializeField] private int playerDamage;
    [SerializeField] private int weaponDamage;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask monsterLayers;
    private SpriteRenderer sprite;
    private Animator anim;
    private PlayerController playerController;
    private Player player;
    private bool isAttackCheck;
    public bool isWearingItem;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isAttackCheck)
        {
            playerController.isAttack = true;
            isAttackCheck = true;

            if (sprite.flipX)
            {
                attackPoint.localPosition = new Vector3(-0.75f, attackPoint.localPosition.y, attackPoint.localPosition.z);
            }
            else
            {
                attackPoint.localPosition = new Vector3(0.75f, attackPoint.localPosition.y, attackPoint.localPosition.z);
            }
            Collider2D[] hitMonsters = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, monsterLayers);

            MonsterTakeDamage(hitMonsters, 1);
        }

        if (Input.GetKeyDown(KeyCode.R) && !isAttackCheck)
        {
            playerController.isSkill = true;
            isAttackCheck = true;

            Collider2D[] hitMonsters = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), attackRange * 3.5f, monsterLayers);

            MonsterTakeDamage(hitMonsters, 2);
        }

        if (isWearingItem)
        {
            isWearingItem = false;
            weaponDamage = 0;
            player.maxHP = 100;

            foreach (Slot s in slot)
            {
                if (!string.IsNullOrEmpty(s.itemName))
                {
                    Item item = Resources.Load<Item>($"Prefabs/itemData/{s.itemName}");
                    if (item != null)
                    {
                        weaponDamage += item.damage;
                        player.maxHP += item.health;
                        Debug.Log(s.itemName);
                    }
                }
            }
            UIManager.instance.SetPlayerHP(player.curHP, player.maxHP);
            UIManager.instance.UpdatePlayerAttackDamage(weaponDamage);
        }
        OnAttackAnimationEnd();
    }

    private void MonsterTakeDamage(Collider2D[] hitMonsters, int damageMultiply)
    {
        foreach (Collider2D monster in hitMonsters)
        {
            if (monster.CompareTag("Skull"))
            {
                StartCoroutine(monster.GetComponent<Skull>().TakeDamage((playerDamage + weaponDamage) * damageMultiply));
            }
            else if (monster.CompareTag("FireWizardSkull"))
            {
                StartCoroutine(monster.GetComponent<FireWizardSkull>().TakeDamage((playerDamage + weaponDamage) * damageMultiply));
            }
            monster.GetComponent<DamageTextController>().ShowDamageText(new Vector3(Random.Range(monster.transform.position.x - 0.25f, monster.transform.position.x + 0.25f), monster.transform.position.y + 0.1f, 0), (playerDamage + weaponDamage) * damageMultiply);
        }
    }

    private void OnAttackAnimationEnd()
    {
        if (playerController.isAttack || playerController.isSkill)
        {
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
            if (animTime >= 0.9f)
            {
                playerController.isAttack = false;
                playerController.isSkill = false;
                isAttackCheck = false;
                anim.Play("Idle");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y), attackRange * 3.5f);
    }
}
