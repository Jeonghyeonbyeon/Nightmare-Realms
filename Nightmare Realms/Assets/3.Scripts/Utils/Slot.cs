using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Inventory inventory;
    private Image slot;
    private Player player;
    public string itemName;
    public bool visible;
    public int idx;

    private static GameObject dragImage;
    private static Image dragImageComponent;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Slot originalSlot;
    private Transform originalParent;

    private void Awake()
    {
        slot = GetComponent<Image>();
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (dragImage == null)
        {
            dragImage = new GameObject("Drag Image");
            dragImageComponent = dragImage.AddComponent<Image>();
            dragImageComponent.raycastTarget = false;
            dragImage.SetActive(false);
            dragImage.transform.SetParent(canvas.transform, false);
        }
        else
        {
            dragImageComponent = dragImage.GetComponent<Image>();
        }
    }

    public void SetVisible(Sprite spr, string name, bool _visible)
    {
        if (slot == null)
        {
            slot = GetComponent<Image>();
        }

        itemName = name;
        visible = _visible;
        slot.sprite = spr;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>().isWearingItem = true;

        if (_visible && spr != null)
        {
            slot.color = new Color(slot.color.r, slot.color.g, slot.color.b, 1f);
        }
        else
        {
            slot.color = new Color(slot.color.r, slot.color.g, slot.color.b, 0f);
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        inventory = FindObjectOfType<Inventory>();

        if (visible)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                // Left Click!
            }
            else if (eventData.button == PointerEventData.InputButton.Middle)
            {
                UseItem();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                DropItem();
            }
        }
    }

    private void UseItem()
    {
        if (!string.IsNullOrEmpty(itemName))
        {
            Item item = Resources.Load<Item>($"Prefabs/itemData/{itemName}");
            if (item != null && item.Tear == "Food")
            {
                player.Heal(item.health);
                SetVisible(null, null, false);
                inventory.cnt--;
            }
        }
    }

    private void DropItem()
    {
        Debug.Log($"Dropping item with name: {itemName}"); 

        GameObject itemPrefab = (GameObject)Resources.Load($"Prefabs/Item/{itemName}");
        GameObject item = Instantiate(itemPrefab, GameObject.Find("Player").transform.position, Quaternion.identity);
        item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        item.name = itemName;

        SetVisible(null, null, false);

        inventory.cnt--;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (visible)
        {
            dragImageComponent.sprite = slot.sprite;
            dragImageComponent.SetNativeSize();
            SetDragImagePosition(eventData.position);
            dragImage.SetActive(true);

            originalSlot = this;
            originalParent = transform.parent;

            canvasGroup.blocksRaycasts = false;
            slot.color = new Color(slot.color.r, slot.color.g, slot.color.b, 0f);  
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (visible)
        {
            SetDragImagePosition(eventData.position);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        inventory = FindObjectOfType<Inventory>();

        if (visible)
        {
            dragImage.SetActive(false);

            Slot targetSlot = GetSlotUnderMouse(eventData);

            if (targetSlot != null && targetSlot != originalSlot)
            {
                if (!targetSlot.visible)
                {
                    targetSlot.SetVisible(originalSlot.slot.sprite, originalSlot.itemName, true);
                    originalSlot.SetVisible(null, null, false);
                }
                else
                {
                    Sprite tempSprite = targetSlot.slot.sprite;
                    string tempName = targetSlot.itemName;

                    targetSlot.SetVisible(originalSlot.slot.sprite, originalSlot.itemName, true);
                    originalSlot.SetVisible(tempSprite, tempName, true);
                }
                if (targetSlot.idx >= 9)
                {
                    inventory.cnt--;
                }
            }
            else
            {
                originalSlot.SetVisible(originalSlot.slot.sprite, originalSlot.itemName, true);
            }

            canvasGroup.blocksRaycasts = true;
            if (slot.sprite != null)
            {
                slot.color = new Color(slot.color.r, slot.color.g, slot.color.b, 1f); 
            }
        }
    }

    private Slot GetSlotUnderMouse(PointerEventData eventData)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = eventData.position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            Slot slot = result.gameObject.GetComponent<Slot>();
            if (slot != null)
            {
                return slot;
            }
        }
        return null;
    }

    private void SetDragImagePosition(Vector2 screenPosition)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPosition, canvas.worldCamera, out Vector2 localPoint);
        dragImage.transform.localPosition = localPoint;
    }
}
