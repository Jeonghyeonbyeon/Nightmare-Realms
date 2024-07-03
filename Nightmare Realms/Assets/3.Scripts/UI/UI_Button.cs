using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Button : MonoBehaviour
{
    private ShopItemSlot shop;

    public void GameStart()
    {
        SceneManager.LoadScene("Stage");
    }

    public void ResetItem()
    {
        if (GameManager.instance.coinCount >= 10)
        {
            GameManager.instance.UpdateCoinCount(-10);
            shop = transform.parent.GetComponent<ShopItemSlot>();
            shop.SetItem();
        }
    }
}