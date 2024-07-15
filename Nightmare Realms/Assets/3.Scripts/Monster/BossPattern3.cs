using UnityEngine;
using System.Collections;

public class BossPattern3 : MonoBehaviour
{
    [SerializeField] private GameObject boneProjectile;
    [SerializeField] private float offsetDistance = 2f;
    [SerializeField] private float fireSpeed = 10f;
    private Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Pattern(Vector3 playerPosition)
    {
        anim.Play("Attack");

        StartCoroutine(FireBones());
    }

    IEnumerator FireBones()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector3 randomOffset = (Vector3)(Random.insideUnitCircle.normalized * offsetDistance);
            Vector3 spawnPosition = playerPosition + randomOffset;
            GameObject bone = Instantiate(boneProjectile, spawnPosition, Quaternion.identity);
            bone.GetComponent<BoneProjectile>().Initialize(fireSpeed);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
