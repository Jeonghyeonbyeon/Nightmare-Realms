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
            Player player = GameObject.Find("Player").GetComponent<Player>();
            player.TakeDamage(player.curHP / 2, 0);

            timer = true;
        }
    }
}