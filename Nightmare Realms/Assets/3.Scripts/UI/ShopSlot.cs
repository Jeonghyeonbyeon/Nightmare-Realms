using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Text NPCText;
    [SerializeField] private Text priceText;
    [SerializeField] private Text tearText;
    private Item itemData;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemData != null)
        {
            NPCText.text = $"{itemData.itemName}\nDamage: {itemData.damage}\nHealth: {itemData.health}";

            switch (itemData.Tear)
            {
                case "Common": tearText.color = new Color(25 / 255f, 255 / 255f, 0 / 255f); break;
                case "Rare": tearText.color = new Color(255 / 255f, 165 / 255f, 0 / 255f); break;
                case "Unique": tearText.color = new Color(0 / 255f, 210 / 255f, 255 / 255f); break;
                case "Legendary": tearText.color = new Color(255 / 255f, 250 / 255f, 0 / 255f); break;
            }
            tearText.text = $"{itemData.Tear}";
        }
    }

    public void SetItemData()
    {
        string imageSprite = GetComponent<Image>().sprite.name;
        itemData = Resources.Load<Item>($"Prefabs/ItemData/{imageSprite}");
        priceText.text = $"{itemData.price}";
    }

    private void Update()
    {
        if (transform.name != "ItemPurchaseButton")
            SetItemData(); 
    }
}
