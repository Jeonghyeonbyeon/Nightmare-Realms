using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Button : MonoBehaviour
{
    private ShopItemSlot shop;
    private bool isGameStart;
    private float time = 0;

    public void GameStart()
    {
        AudioClip clip = SoundManager.instance.click;
        SoundManager.instance.PlaySFX_1(clip);

        isGameStart = true;
    }

    public void ResetItem()
    {
        if (GameManager.instance.coinCount >= 10)
        {
            AudioClip clip = SoundManager.instance.shopSpin;
            SoundManager.instance.PlaySFX_1(clip);
            GameManager.instance.UpdateCoinCount(-10);
            shop = transform.parent.GetComponent<ShopItemSlot>();
            shop.SetItem();
        }
    }

    public void ResetItemNextScene()
    {
        shop = transform.parent.GetComponent<ShopItemSlot>();
        shop.SetItem();
    }

    public void GameExit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (isGameStart)
        {
            time += Time.deltaTime;

            if (time > 1.0f)
            {
                time = 0;
                isGameStart = false;
                SceneManager.LoadScene("GameStartCutScene");
            }
        }
    }
}