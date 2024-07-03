using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    [SerializeField] private Image[] itemSlot = new Image[3];
    [SerializeField] private Item[] itemData;
    [SerializeField] private ShopSlot[] slot = new ShopSlot[3];

    private void Start()
    {
        SetItem();

        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i] != null)
            {
                slot[i].SetItemData();
            }
        }
    }

    public void SetItem()
    {
        List<Item> itemList = new List<Item>(itemData);
        ShuffleList(itemList);

        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].sprite = itemList[i].sprite;
        }
    }

    void ShuffleList(List<Item> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randIndex = Random.Range(i, list.Count);
            Item temp = list[i];
            list[i] = list[randIndex];
            list[randIndex] = temp;
        }
    }
}
