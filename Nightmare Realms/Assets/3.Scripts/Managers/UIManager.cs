using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject inventory;
    public GameObject shop;
    public GameObject dead;
    public Text coinText;
    public Text playerHPText;
    public Text currentMonsterCount;
    public Text currnetPlayerAttackDamage;
    public Text dashCoolText;
    public Text skillCoolText;
    public Text stageInfoText;
    public Text anyKeyText;
    public Slider playerHPSlider;
    public Slider playerManaSlider;
    public Slider bossHPSlider;
    public Image dashIcon;
    public Image skillIcon;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StageInfoUpdate();
    }

    public void StageInfoUpdate()
    {
        int stage = GameManager.instance.stage;
        if (stage > 4)
        {
            stageInfoText.text = $"Stage Boss\n{Resources.Load<Stage>($"Prefabs/StageData/Stage_Boss").stageName}";
        }
        else
        {
            stageInfoText.text = $"Stage {stage}\n{Resources.Load<Stage>($"Prefabs/StageData/Stage_{stage}").stageName}";
        }
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
        playerHPSlider.value = (float)cur / max;
        playerHPText.text = $"{cur}/{max}";
    }

    public void SetPlayerMana(int cur, int max)
    {
        if (cur >= max)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().curMana = max;
            cur = max;
        }
        playerManaSlider.value = (float)cur / max;
    }
}
