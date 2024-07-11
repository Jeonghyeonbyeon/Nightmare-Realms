using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.UpdateCoinCount(1);
            AudioClip coin = SoundManager.instance.coin;
            SoundManager.instance.PlaySFX_1(coin);
            Destroy(gameObject);
        }
    }
}
