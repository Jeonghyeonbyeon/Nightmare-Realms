using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private List<Slot> ItemList = new List<Slot>();
    private Collider2D currentHit;
    private bool isInventory;
    public int cnt;

    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 2, layerMask);

        if (hits.Length > 0)
        {
            Collider2D closestHit = hits[0];
            float closestDistance = Vector2.Distance(transform.position, closestHit.transform.position);

            foreach (Collider2D hit in hits)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestHit = hit;
                    closestDistance = distance;
                }
            }

            if (currentHit == null || currentHit != closestHit)
            {
                if (currentHit != null)
                {
                    currentHit.transform.GetChild(0).gameObject.SetActive(false);
                }

                currentHit = closestHit;
                currentHit.transform.GetChild(0).gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                AddItem(currentHit);
            }
        }
        else
        {
            if (currentHit != null)
            {
                currentHit.transform.GetChild(0).gameObject.SetActive(false);
                currentHit = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isInventory = !isInventory;
            UIManager.instance.inventory.SetActive(isInventory);
            UIManager.instance.currnetPlayerAttackDamage.gameObject.SetActive(isInventory);
        }
    }

    void AddItem(Collider2D hit)
    {
        if (cnt >= 9)
        {
            return;
        }

        int idx = 0;

        for (; idx < ItemList.Count; idx++)
            if (!ItemList[idx].visible)
                break;

        if (idx < ItemList.Count)
        {
            Sprite itemSprite = hit.GetComponent<SpriteRenderer>().sprite;
            string itemName = hit.name;
            Debug.Log($"Adding item with name: {itemName}"); // 디버그 로그 추가
            ItemList[idx].SetVisible(itemSprite, itemName, true); // name 파라미터가 hit.name으로 제대로 전달되는지 확인
            Destroy(hit.gameObject);
            cnt++;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2);
    }
}
