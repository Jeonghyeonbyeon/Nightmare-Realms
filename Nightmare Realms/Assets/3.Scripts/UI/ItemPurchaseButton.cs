using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class ItemPurchaseButton : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Transform itemSpawnPoint;
    private Item itemData;

    void Update()
    {
        string itemName = itemImage.sprite.name;
        itemData = Resources.Load<Item>($"Prefabs/ItemData/{itemName}");
        Debug.Log(itemName);
    }

    public void ItemPurchase()
    {
        if (GameManager.instance.coinCount >= itemData.price)
        {
            GameManager.instance.UpdateCoinCount(-itemData.price);
            char firstChar = char.ToLower(itemData.name[0]);
            string restOfString = itemData.name.Substring(1);
            string itemName = firstChar + restOfString;
            GameObject item = Instantiate(Resources.Load<GameObject>($"Prefabs/Item/{itemName}"), itemSpawnPoint.position, Quaternion.identity);
            item.name = itemName;
        }
    }
}
