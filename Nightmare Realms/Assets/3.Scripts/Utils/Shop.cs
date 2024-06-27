using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private bool isShop;
    private bool isPlayer = true;

    private void Update()
    {
        transform.GetChild(0).gameObject.SetActive(isShop);

        if (Input.GetKeyDown(KeyCode.F) && isShop)
        {
            isPlayer = !isPlayer;

            if (!UIManager.instance.shop.gameObject.activeSelf)
            {
                UIManager.instance.shop.gameObject.SetActive(true);
            }
            else
            {
                UIManager.instance.shop.gameObject.SetActive(false);
            }
            FindObjectOfType<PlayerController>().enabled = isPlayer;
            FindObjectOfType<PlayerController>().gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            FindObjectOfType<PlayerDash>().enabled = isPlayer;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isShop = !isShop;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isShop = !isShop;
        }
    }
}
