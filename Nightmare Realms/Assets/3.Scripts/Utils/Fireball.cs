using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Player player;
    private Vector3 targetDirection;
    public int damage;
    public float speed = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetDirection = (player.transform.position - transform.position).normalized;
    }

    void Update()
    {
        transform.position += targetDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("피해 입음!");
            player.TakeDamage(damage, 0f);
            Destroy(gameObject, 0.1f);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}