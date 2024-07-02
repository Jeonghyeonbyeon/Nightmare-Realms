using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer sprite;
    public int curHP;
    public int maxHP;
    private bool isTakingDamage = false;
    private float damageTimer = 0f;
    public float damageDuration;  

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
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
        Debug.Log("Player has died");
    }

    public void Heal(int amount)
    {
        curHP += amount;

        if (curHP > maxHP)
            curHP = maxHP;

        UIManager.instance.SetPlayerHP(curHP, maxHP);
    }
}
