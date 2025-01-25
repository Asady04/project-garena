using UnityEngine;

public class OneTimeController : MonoBehaviour
{
    private bool crack = false;

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!crack)
            {
                crack = true;
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
