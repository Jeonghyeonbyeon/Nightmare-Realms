using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float speed = 5f;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radius, LayerMask.GetMask("Coin"));

        foreach (Collider2D hit in hits)
        { 
            if (hit.CompareTag("Coin"))
            {
                hit.GetComponent<Collider2D>().isTrigger = true;

                StartCoroutine(MoveCoinTowardsPlayer(hit.transform));
            }
        }
    }

    private IEnumerator MoveCoinTowardsPlayer(Transform coinTransform)
    {
        while (coinTransform != null && Vector2.Distance(coinTransform.position, playerTransform.position) > 0.1f)
        {
            if (coinTransform != null)
            {
                coinTransform.position = Vector2.MoveTowards(coinTransform.position, playerTransform.position, speed * Time.deltaTime);
            }
            yield return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
