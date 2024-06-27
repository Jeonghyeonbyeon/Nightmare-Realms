using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private float ghostDelay = 0.1f;
    private float ghostDelayTimer;

    void Update()
    {
        if (ghostDelayTimer > 0)
        {
            ghostDelayTimer -= Time.deltaTime;
        }
        else
        {
            CreateGhost();
            ghostDelayTimer = ghostDelay;
        }
    }

    void CreateGhost()
    {
        GameObject ghost = Instantiate(ghostPrefab, transform.position, transform.rotation);
        SpriteRenderer sr = ghost.GetComponent<SpriteRenderer>();
        sr.sprite = GetComponent<SpriteRenderer>().sprite;
        sr.flipX = GetComponent<SpriteRenderer>().flipX;

        Color color = sr.color;
        color.a = 0.5f; 
        sr.color = color;

        Destroy(ghost, 0.5f);
    }
}
