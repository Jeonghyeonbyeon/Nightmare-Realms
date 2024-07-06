using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextController : MonoBehaviour
{
    private GameObject damageTextPrefab;

    public void ShowDamageText(Vector3 position, int damageAmount)
    {
        GameObject damageTextInstance = Instantiate(Resources.Load<GameObject>("Prefabs/Utils/DamageEffectText"), transform.Find("Canvas").transform);
        damageTextInstance.transform.position = position;

        Text damgeText = damageTextInstance.GetComponent<Text>();
        damgeText.text = damageAmount.ToString();

        StartCoroutine(DamageTextDestory(damgeText));
    }

    IEnumerator DamageTextDestory(Text textComponent)
    {
        float duration = 1f;
        float alpha = 1f;
        Color textColor = textComponent.color;
        Vector3 originalScale = textComponent.transform.localScale;
        Vector3 targetScale = originalScale * 2f;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            textColor.a = alpha;
            textComponent.color = textColor;
            textComponent.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            textComponent.transform.Translate(Vector3.up * Time.deltaTime);

            yield return null;
        }
        Destroy(textComponent.gameObject);
    }
}