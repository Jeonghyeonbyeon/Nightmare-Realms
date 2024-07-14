using UnityEngine;
using System.Collections;

public class BossPattern2 : MonoBehaviour
{
    public float chargeSpeed = 10f;
    private Vector3 targetPosition;

    public void Pattern(Vector3 playerPosition)
    {
        targetPosition = playerPosition;
        StartCoroutine(Charge());
    }

    IEnumerator Charge()
    {
        yield return new WaitForSeconds(1f);
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, chargeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}