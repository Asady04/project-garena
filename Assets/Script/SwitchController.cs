using UnityEngine;

public class SwitchController : MonoBehaviour
{
    private SwitchPlatController spc;
    public SpriteRenderer spriteRenderer;
    public Sprite spriteON, spriteOFF;
    private int playerColCount = 0;

    void Start()
    {
        spc = GetComponentInChildren<SwitchPlatController>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerColCount++;
            if (playerColCount == 1)
            {
                spc.boxCol2d.enabled = !spc.boxCol2d.enabled;
                spc.spriteRenderer.enabled = !spc.spriteRenderer.enabled;
                if (spriteRenderer != null && spriteON != null)
                {
                    spriteRenderer.sprite = spriteON;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D col2d)
    {
        if (col2d.gameObject.CompareTag("Player"))
        {
            playerColCount--;
            if (playerColCount == 0)
            {
                spc.boxCol2d.enabled = !spc.boxCol2d.enabled;
                spc.spriteRenderer.enabled = !spc.spriteRenderer.enabled;
                if (spriteRenderer != null && spriteOFF != null)
                {
                    spriteRenderer.sprite = spriteOFF;
                }
            }
        }
    }
}
