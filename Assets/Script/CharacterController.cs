using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb; // Reference to the character's Rigidbody2D
    public bool hasTeleported = false;
    private PlatformController platformController;
    private BlueController blueController;
    public bool isBlue;
    public bool inTP;
    private SoundManager soundManager;

    void Start()
    {
        // Get references
        platformController = FindFirstObjectByType<PlatformController>();
        blueController = FindFirstObjectByType<BlueController>();
        rb = GetComponent<Rigidbody2D>();
        soundManager = FindFirstObjectByType<SoundManager>();
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Portal"))
        {
            hasTeleported = false;
        }
    }
    void Update()
    {
        if (gameObject.transform.position.y < platformController.platform[0].position.y)
        {
            platformController.GameOver();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for different tags
        if (collision.gameObject.CompareTag("Trampoline"))
        {
            Jump(collision.gameObject.GetComponent<TrampolineController>().jumpforce);
        }
        else if (collision.gameObject.CompareTag("Piston"))
        {
            Boost(collision.gameObject.GetComponent<PistonController>().skipPlatform);
        }
        else if (collision.gameObject.CompareTag("Portal") && !hasTeleported)
        {
            inTP = true;
            var portal = collision.gameObject.GetComponent<PortalController>();
            if (portal != null && portal.destinationPortal != null)
            {
                Teleport(portal);
            }
        }
        else if (collision.gameObject.CompareTag("Blue Post"))
        {
            if (isBlue)
            {
                blueController.stop = true;
            }
        }
        else if (collision.gameObject.CompareTag("Red Post"))
        {
            StartCoroutine(DelayBeforeCheck());
            if (!isBlue && blueController.stop)
            {
                platformController.stop = true;
                StartCoroutine(DelayToWin());

            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (!inTP)
            {
                platformController.GameOver();
            }
        }
    }
    IEnumerator DelayToWin()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(0.5f);

        platformController.Win();
    }
    IEnumerator DelayToTP()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(3f);
        inTP = false;
    }
    void Jump(float jumpForce)
    {
        // Apply upward force for the jump
        soundManager.Trampoline();
        rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
    }

    void Boost(int pistonForce)
    {
        // Update platform index safely
        if (isBlue)
        {
            blueController.platformIndex = Mathf.Clamp(
            blueController.platformIndex + pistonForce,
            0,
            platformController.platform.Length - 1
            );
        }
        else
        {
            platformController.platformIndexToStart = Mathf.Clamp(
            platformController.platformIndexToStart + pistonForce,
            0,
            platformController.platform.Length - 1
            );
        }
    }

    void Teleport(PortalController portal)
    {
        if (!portal.destinationPortal.isTeleporting)
        {
            if (isBlue)
            {
                blueController.AfterTP(portal.destinationPortal.platformIndex);

            }
            else
            {
                platformController.AfterTP(portal.destinationPortal.platformIndex);
            }
            soundManager.Portal();
            // Mark the destination portal as teleporting
            portal.destinationPortal.destinationPortal.isTeleporting = true;
            // Teleport character to destination portal
            transform.position = new Vector3(
                portal.destinationPortal.transform.position.x,
                portal.destinationPortal.transform.position.y + 1f, // Slight offset to avoid collision
                portal.destinationPortal.transform.position.z
            );


            // Update platform index

            // Mark as teleported to prevent immediate re-teleportation
            hasTeleported = true;
            StartCoroutine(DelayToTP());
        }
    }
    IEnumerator DelayBeforeCheck()
    {
        yield return new WaitForSeconds(1f);
    }
}