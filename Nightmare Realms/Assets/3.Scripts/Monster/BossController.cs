using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    [SerializeField] private BossPattern1 pattern1;
    [SerializeField] private BossPattern2 pattern2;
    [SerializeField] private BossPattern3 pattern3;
    [SerializeField] private BossPattern4 pattern4;
    [SerializeField] private MonsterData monsterData;
    [SerializeField] private Transform player;
    private SpriteRenderer sprite;
    private Animator anim;
    public int curHP;
    public int maxHP;
    private float time = 10f;
    private bool isDead;

    private void Start()
    {
        curHP = monsterData.curHP;
        maxHP = monsterData.maxHP;

        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        StartCoroutine(Patterns());
    }

    IEnumerator Patterns()
    {
        while (true)
        {
            if (!isDead)
            {
                if (curHP <= (maxHP / 2))
                {
                    if (sprite != null)
                    {
                        sprite.color = new Color(200 / 255f, 110 / 255f, 255 / 255f);
                        time = 5f;
                    }
                }
                else
                {
                    time = 10f;
                }
                yield return new WaitForSeconds(time);
                anim.Play("Idle");
                int index = Random.Range(0, 4);
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
                    case 3:
                        pattern4.Pattern();
                        break;
                }
            }
            else
            {
                yield break;
            }
        }
    }

    public IEnumerator TakeDamage(int damage)
    {
        AudioClip clip = SoundManager.instance.swordHit;
        SoundManager.instance.PlaySFX_2(clip);

        anim.Play("TakeDamage");

        curHP -= damage;
        SetHPSliderValue();

        if (sprite != null)
            sprite.color = Color.red;
        else
            Debug.LogWarning("SpriteRenderer is null or destroyed");

        yield return new WaitForSecondsRealtime(0.3f);

        if (sprite != null)
            sprite.color = new Color(150 / 255f, 150 / 255f, 150 / 255f);
        else
            Debug.LogWarning("SpriteRenderer is null or destroyed");

        anim.Play("Idle");

        if (curHP <= 0)
        {
            isDead = true;
        }
    }

    private void SetHPSliderValue()
    {
        UIManager.instance.bossHPSlider.value = (float)curHP / maxHP;
    }

    private void Dead()
    {
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        float shakeDuration = 5f;
        float shakeMagnitude = 0.1f;
        Vector3 pos = transform.localPosition;

        for (float t = 0; t < shakeDuration; t += Time.deltaTime)
        {
            float x = Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.localPosition = new Vector3(pos.x + x, pos.y + y, pos.z);
            yield return null;
        }
        transform.localPosition = pos;

        float fadeDuration = 2f;
        float timer = 0f;
        Color color = sprite.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            sprite.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        Destroy(gameObject);
    }
}