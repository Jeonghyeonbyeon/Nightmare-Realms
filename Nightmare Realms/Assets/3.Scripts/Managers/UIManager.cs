using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject inventory;
    public GameObject shop;
    public Text coinText;
    public Text playerHPText;
    public Text currentMonsterCount;
    public Text currnetPlayerAttackDamage;
    public Slider playerHP;

    private void Awake()
    {
        instance = this;
    }

    public void UpdatePlayerAttackDamage(int damage)
    {
        currnetPlayerAttackDamage.text = $"Damage : {damage}";
    }

    public void SetPlayerHP(int cur, int max)
    {
        if (cur >= max)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().curHP = max;
            cur = max;
        }
        playerHP.value = (float)cur / max;
        playerHPText.text = $"{cur}/{max}";
    }
}
