using UnityEngine;
using System.Collections;
using TMPro;

public class DestroyFade : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private TextMeshProUGUI uiText;
    public float fadeDuration = 1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        uiText = GetComponent<TextMeshProUGUI>();
    }

    public void DestroyObject()
    {
        if (uiText != null)
        {
            StartCoroutine(FadeAndDestroyText(uiText));
        }
        else if (spriteRenderer != null)
        {
            StartCoroutine(FadeAndDestroy(spriteRenderer));
        }

    }

    private IEnumerator FadeAndDestroy(SpriteRenderer item)
    {
        float elapsedTime = 0f;
        Color initialColor = item.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Calculate new alpha value
            item.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha); // Apply new alpha
            yield return null;
        }
        Destroy(gameObject);
    }

    private IEnumerator FadeAndDestroyText(TextMeshProUGUI item)
    {
        float elapsedTime = 0f;
        Color initialColor = item.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Calculate new alpha value
            item.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha); // Apply new alpha
            yield return null;
        }
        Destroy(gameObject);
    }
}
