using UnityEngine;

public class OneTimeController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool crack = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!crack)
            {
                crack = true;
                spriteRenderer.color = new Color(1f, 0.6f, 0f);
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
