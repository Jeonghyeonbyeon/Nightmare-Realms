using UnityEngine;
using System.Collections;

public class BoneProjectile : MonoBehaviour
{
    public float lifetime = 5f; 
    private Transform playerTransform;
    private float speed;

    public void Initialize(float fireSpeed)
    {
        speed = fireSpeed;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(FacePlayerAndLaunch());
    }

    IEnumerator FacePlayerAndLaunch()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        GetComponent<Rigidbody2D>().velocity = transform.up * speed; 
        Destroy(gameObject, lifetime);
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
