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
                spc.boxCol2d.enabled = true;
                spc.spriteRenderer.color = new Color(0.4f, 0.4f, 0.4f);
                if (spriteRenderer!= null && spriteON != null)
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
                spc.boxCol2d.enabled = false;
                spc.spriteRenderer.color = new Color(0.125f, 0.125f, 0.125f);
                if (spriteRenderer!= null && spriteOFF != null)
                {
                    spriteRenderer.sprite = spriteOFF;
                }
            }
        }
    }
}
