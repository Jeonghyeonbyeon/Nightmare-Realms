using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer sprite;
    public int curHP;
    public int maxHP;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        UIManager.instance.SetPlayerHP(curHP, maxHP);
    }

    public IEnumerator TakeDamage(int damage, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        curHP -= damage;

        if (curHP <= 0)
        {
            curHP = 0;
            Dead();
        }
        Color color = Color.red;
        sprite.color = color;
        yield return new WaitForSecondsRealtime(0.15f);
        color = Color.white;
        sprite.color = color;
        UIManager.instance.SetPlayerHP(curHP, maxHP);
    }

    private void Dead()
    {
        
    }

    public void Heal(int amount)
    {
        curHP += amount;

        if (curHP > maxHP)
            curHP = maxHP;

        UIManager.instance.SetPlayerHP(curHP, maxHP);
    }
}
