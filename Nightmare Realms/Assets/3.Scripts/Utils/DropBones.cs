using UnityEngine;
using System.Collections;

public class DropBones : MonoBehaviour
{
    public float lifetime = 10f; 
    public float gravityScale = 1f; 

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = gravityScale;
        }
        Destroy(gameObject, lifetime);
    }
}
