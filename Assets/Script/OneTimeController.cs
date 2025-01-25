using UnityEngine;

public class OneTimeController : MonoBehaviour
{
    private bool crack = false;
    public Sprite newSprite;  // The sprite you want to change to
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!crack)
            {
                crack = true;
                if (spriteRenderer != null && newSprite != null)
                {
                    spriteRenderer.sprite = newSprite;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (crack)
            {
                Destroy(gameObject);
            }
        }
    }
}
