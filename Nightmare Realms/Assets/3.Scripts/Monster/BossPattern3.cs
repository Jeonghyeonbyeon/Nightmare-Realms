using UnityEngine;
using System.Collections;

public class BossPattern3 : MonoBehaviour
{
    public GameObject boneProjectile;
    public float offsetDistance = 2f;
    public float fireSpeed = 10f;

    public void Pattern(Vector3 playerPosition)
    {
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
