using UnityEngine;

public class PortalController : MonoBehaviour
{
    public PortalController destinationPortal; // Reference to the paired portal
    public bool isTeleporting = false; // Prevent infinite teleportation loops
    public int platformIndex = 0; // Platform index for the destination portal
    public Animator animator;  // Reference to the Animator

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("debug albaghdadi");
            animator.SetBool("isTeleporting", true);
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isTeleporting", false);
            Debug.Log("debug miaw");
            isTeleporting = false;
        }
    }
}
