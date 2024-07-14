using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private SpriteRenderer sprite;
    private PlayerController player;
    private Animator anim;
    public int curHP;
    public int maxHP;
    public int curMana;
    public int maxMana;
    private bool isTakingDamage = false;
    private float damageTimer = 0f;
    private float Timer = 0f;
    public float damageDuration;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        UIManager.instance.SetPlayerHP(curHP, maxHP);
    }

    private void Update()
    {
        if (isTakingDamage)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageDuration)
            {
                isTakingDamage = false;
                damageTimer = 0f;
                sprite.color = new Color(160 / 255f, 160 / 255f, 160 / 255f);
                UIManager.instance.SetPlayerHP(curHP, maxHP);
            }
        }
        Timer += Time.deltaTime;

        if (Timer >= 1f)
        {
            Timer = 0;
            curMana += 2;
            UIManager.instance.SetPlayerMana(curMana, maxMana);
        }
    }

    public void TakeDamage(int damage, float time)
    {
        if (!player.isDead)
        {
            //AudioClip clip = SoundManager.instance.takeDamage;
            //SoundManager.instance.PlaySFX_1(clip);
            StartCoroutine(TakeDamageCoroutine(damage, time));
            return;
        }
        Debug.Log("Player Dead!");
    }

    private IEnumerator TakeDamageCoroutine(int damage, float time)
    {
        yield return new WaitForSecondsRealtime(time);

        if (curHP > maxHP)
            curHP = maxHP;

        curHP -= damage;

        if (curHP <= 0)
        {
            curHP = 0;
            Dead();
        }
        sprite.color = Color.red;
        isTakingDamage = true;
    }

    private void Dead()
    {
        player.isDead = true;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerDash>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        anim.Play("Dead");

        StartCoroutine(WaitForAnimationAndFadeIn());
    }

    private IEnumerator WaitForAnimationAndFadeIn()
    {
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            yield return null;
        }
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }
        StartCoroutine(FadeInDeathUI());
    }

    private IEnumerator FadeInDeathUI()
    {
        UIManager.instance.dead.gameObject.SetActive(true);
        Image deadImage = UIManager.instance.dead.GetComponent<Image>();

        if (deadImage != null)
        {
            float elapsedTime = 0f;
            float fadeDuration = 5f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                Color newColor = deadImage.color;
                newColor.a = alpha;
                deadImage.color = newColor;
                yield return null;
            }
            UIManager.instance.anyKeyText.gameObject.SetActive(true);
            UIManager.instance.anyKeyText.text = "아무 키나 눌러서 재시작하세요.";

            yield return new WaitUntil(() => Input.anyKeyDown);

            AudioClip clip = SoundManager.instance.bgmMain;
            SoundManager.instance.PlayBGMLoop(clip);
            SceneManager.LoadScene("MainScene");
        }
    }

    public void Heal(int amount)
    {
        curHP += amount;

        if (curHP > maxHP)
            curHP = maxHP;

        UIManager.instance.SetPlayerHP(curHP, maxHP);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bones"))
        {
            TakeDamage(20, 0f);
            Destroy(collision.gameObject);
        }
    }
}
