using UnityEngine;

public class SwitchController : MonoBehaviour
{
    private SwitchPlatController spc;
    private SpriteRenderer spriteRenderer;
    private bool state = false;

    void Start()
    {
        spc = GetComponentInChildren<SwitchPlatController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (state)
            {
                spc.boxCol2d.enabled = false;
                spc.spriteRenderer.color = new Color(0.125f, 0.125f, 0.125f);
                state = false;
                spriteRenderer.color = new Color(0f, 0.4f, 0f);
            }
            else
            {
                spc.boxCol2d.enabled = true;
                spc.spriteRenderer.color = new Color(0.4f, 0.4f, 0.4f);
                state = true;
                spriteRenderer.color = new Color(0f, 1f, 0f);
            }
        }
    }
}
