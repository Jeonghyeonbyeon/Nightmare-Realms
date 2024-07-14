using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public BossPattern1 pattern1;
    public BossPattern2 pattern2;
    public BossPattern3 pattern3;
    public Transform player;

    private void Start()
    {
        StartCoroutine(Patterns());
    }

    IEnumerator Patterns()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            int index = Random.Range(0, 3);
            switch (index)
            {
                case 0:
                    pattern1.StartCoroutine(pattern1.PatternRoutine());
                    break;
                case 1:
                    pattern2.Pattern(player.position);
                    break;
                case 2:
                    pattern3.Pattern(player.position);
                    break;
            }
        }
    }
}