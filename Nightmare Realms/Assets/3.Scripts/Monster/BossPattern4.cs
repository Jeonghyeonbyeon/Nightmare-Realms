using UnityEngine;
using System.Collections;

public class BossPattern4 : MonoBehaviour
{
    [SerializeField] private Transform[] targetPoints; 
    [SerializeField] private GameObject bonePrefab;
    [SerializeField] private float xRange = 30f; 
    [SerializeField] private float boneSpacing = 1f; 
    [SerializeField] private float yOffset = 20f;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Pattern()
    {
        StartCoroutine(FireBones());
    }

    IEnumerator FireBones()
    {
        Transform targetPoint = targetPoints[Random.Range(0, targetPoints.Length)];
        Vector3 spawnPosition;

        anim.Play("Attack");

        for (float x = -xRange; x <= xRange; x += boneSpacing)
        {
            if (Mathf.Abs(x) > boneSpacing)
            {
                spawnPosition = new Vector3(targetPoint.position.x + x, targetPoint.position.y + yOffset, targetPoint.position.z);
                Instantiate(bonePrefab, spawnPosition, Quaternion.identity);
            }
        }
        anim.Play("Idle");
        yield return null;
    }
}
