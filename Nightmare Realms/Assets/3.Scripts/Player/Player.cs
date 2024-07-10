using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer sprite;
    private PlayerController player;
    public int curHP;
    public int maxHP;
    public int curMana;
    public int maxMana;
    private bool isTakingDamage = false;
    private float damageTimer = 0f;
    private float Timer = 0f;
    public float damageDuration;  

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GetComponent<PlayerController>();
        UIManager.instance.SetPlayerHP(curHP, maxHP);
    }

    private void Update()
    {
        if (isTakingDamage)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageDuration)
            {
                isTakingDamage = false;
                damageTimer = 0f;
                sprite.color = new Color(160 / 255f, 160 / 255f, 160 / 255f);
                UIManager.instance.SetPlayerHP(curHP, maxHP);
            }
        }
        Timer += Time.deltaTime;

        if (Timer >= 1f)
        {
            Timer = 0;
            curMana += 2;
            UIManager.instance.SetPlayerMana(curMana, maxMana);
        }
    }

    public void TakeDamage(int damage, float time)
    {
        StartCoroutine(TakeDamageCoroutine(damage, time));
    }

    private IEnumerator TakeDamageCoroutine(int damage, float time)
    {
        yield return new WaitForSecondsRealtime(time);

        if (curHP > maxHP)
            curHP = maxHP;

        curHP -= damage;

        if (curHP <= 0)
        {
            curHP = 0;
            Dead();
        }
        sprite.color = Color.red;
        isTakingDamage = true;
    }

    private void Dead()
    {
        player.isDead = true;
    }

    public void Heal(int amount)
    {
        curHP += amount;

        if (curHP > maxHP)
            curHP = maxHP;

        UIManager.instance.SetPlayerHP(curHP, maxHP);
    }
}
