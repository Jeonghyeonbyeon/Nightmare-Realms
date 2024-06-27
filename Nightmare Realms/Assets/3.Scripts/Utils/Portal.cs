using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform portalPoint;
    [SerializeField] private float radius;
    [SerializeField] private float pullSpeed;
    [SerializeField] private float shakeMagnitude;
    [SerializeField] private float shakeSpeed;

    private void Update()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(new Vector2(portalPoint.position.x, portalPoint.position.y), radius, LayerMask.GetMask("Player"));

        if (hitPlayer != null)
        {
            StartCoroutine(PullPlayerToPortal(hitPlayer));
        }
    }

    private IEnumerator PullPlayerToPortal(Collider2D player)
    {
        player.GetComponent<PlayerController>().enabled = false; 
        player.GetComponent<PlayerDash>().enabled = false;
        player.GetComponent<GhostEffect>().enabled = false;
        player.GetComponent<Animator>().Play("Dash");
        Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
        rigid.velocity = Vector2.zero;

        Vector2 start = player.transform.position;
        Vector2 target = portalPoint.position;
        float time = 0;

        while (Vector2.Distance(player.transform.position, portalPoint.position) > 0.1f)
        {
            time += Time.deltaTime;
            Vector2 current = Vector2.Lerp(start, target, time * pullSpeed);
            current.y += Mathf.Sin(time * shakeSpeed) * shakeMagnitude;
            player.transform.Rotate(0f, 0f, -0.01f);
            player.transform.localScale -= new Vector3(0.0000025f, 0.0000025f, 0f);
            player.transform.position = current;

            yield return null;
        }
        player.transform.position = target;
        SceneManager.LoadScene(GameManager.instance.stage++);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int _stage = GameManager.instance.stage + 1;
            SceneManager.LoadScene($"Stage_{_stage}");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector2(portalPoint.position.x, portalPoint.position.y), radius);
    }
}
