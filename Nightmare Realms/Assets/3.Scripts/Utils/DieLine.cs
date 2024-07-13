using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieLine : MonoBehaviour
{
    private bool timer;
    private float time = 0;

    private void Update()
    {
        if (timer)
        {
            time += Time.deltaTime;

            if (time > 3.5f)
            {
                timer = false;
                time = 0;
                GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
                GameObject.Find("Player").GetComponent<PlayerDash>().enabled = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Player").transform.position = Resources.Load<Stage>($"Prefabs/StageData/Stage_{GameManager.instance.stage}").spawnPos;
            GameObject.Find("Player").GetComponent<Animator>().Play("Idle");
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
            GameObject.Find("Player").GetComponent<PlayerDash>().enabled = false;
            GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX = false;
            GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if (player.curHP > 1)
                player.TakeDamage(player.curHP / 2, 0);
            else
                player.TakeDamage(1, 0);

            timer = true;
        }
        else if (collision.gameObject.CompareTag("Skull") || collision.gameObject.CompareTag("FireWizardSkull") || collision.gameObject.CompareTag("DarkWizardSkull") || collision.gameObject.CompareTag("LeatherSkull"))
        {
            GameManager.instance.UpdateMonsterCount(1);
            Destroy(collision.gameObject);
        }
    }
}