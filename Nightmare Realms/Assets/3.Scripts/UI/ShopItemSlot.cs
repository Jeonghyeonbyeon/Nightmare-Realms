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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SetItem();
        }
    }

    void SetItem()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].sprite = itemData[Random.Range(0, itemData.Length)].sprite;
        }
    }
}