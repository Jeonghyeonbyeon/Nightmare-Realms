using UnityEngine;
using System.Collections;

public class BossPattern1 : MonoBehaviour
{
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private GameObject bonePrefab;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float fireInterval = 0.5f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        StartCoroutine(PatternRoutine());
    }

    public IEnumerator PatternRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            anim.Play("Idle");

            Transform targetPoint = movePoints[Random.Range(0, movePoints.Length)];
            yield return StartCoroutine(MoveToPoint(targetPoint));
            yield return StartCoroutine(FireBones());
        }
    }

    IEnumerator MoveToPoint(Transform target)
    {
        while (Vector3.Distance(transform.position, target.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator FireBones()
    {
        anim.Play("Attack");

        for (float i = 0; i < 360; i += 22.5f) 
        {
            Quaternion rotation = Quaternion.Euler(0, 0, i);
            GameObject bone = Instantiate(bonePrefab, transform.position, rotation);
            bone.GetComponent<Rigidbody2D>().velocity = bone.transform.up * 5f; 
            yield return new WaitForSeconds(fireInterval);
        }
    }
}
