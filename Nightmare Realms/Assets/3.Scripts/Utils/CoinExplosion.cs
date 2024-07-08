using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinExplosion : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;

    public void ExplodeCoins(int coinCount, float explosionForce, float radius)
    {
        for (int i = 0; i < coinCount; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                rb.AddForce(randomDirection * explosionForce, ForceMode2D.Impulse);
            }
        }
    }
}