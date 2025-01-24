using UnityEngine;

public class PortalController : MonoBehaviour
{
    public PortalController destinationPortal; // Reference to the paired portal
    public bool isTeleporting = false; // Prevent infinite teleportation loops
    public int platformIndex = 0; // Platform index for the destination portal

    void OnCollisionExit2D(Collision2D other){
        if (other.gameObject.CompareTag("Player"))
        {
            isTeleporting = false;
        }
    }
}
