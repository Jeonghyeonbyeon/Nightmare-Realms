using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int stage;
    public int coinCount;
    public int monsterCount;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        AudioClip clip = SoundManager.instance.bgmStage_1;
        SoundManager.instance.PlayBGMLoop(clip);

        SetMonsterCount(stage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
            UpdateMonsterCount(1);
        else if (Input.GetKeyDown(KeyCode.Q))
            stage++;
    }

    public void SetMonsterCount(int stage)
    {
        int count;

        if (stage > 4)
        {
            count = Resources.Load<Stage>($"Prefabs/StageData/Stage_Boss").monsterCount;
        }
        else
        {
            count = Resources.Load<Stage>($"Prefabs/StageData/Stage_{stage}").monsterCount;
        }
        monsterCount = count;
        UIManager.instance.currentMonsterCount.text = monsterCount.ToString();
    }

    public void UpdateMonsterCount(int count)
    {
        monsterCount -= count;
        UIManager.instance.currentMonsterCount.text = monsterCount.ToString();

        if (monsterCount <= 0)
        {
            GameObject.Find("Portal").transform.GetChild(stage - 1).gameObject.SetActive(true);
        }
    }

    public void UpdateCoinCount(int count)
    {
        coinCount += count;
        UIManager.instance.coinText.text = $"{coinCount}";
    }
}
