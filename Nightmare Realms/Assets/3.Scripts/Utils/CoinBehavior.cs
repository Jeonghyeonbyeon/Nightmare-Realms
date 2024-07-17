using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    [SerializeField] private LayerMask layer;

    private void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), 0.5f, layer);

        if (hit != null)
        {
            GameManager.instance.UpdateCoinCount(1);
            AudioClip coin = SoundManager.instance.coin;
            SoundManager.instance.PlaySFX_1(coin);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y), 0.5f);
    }
}