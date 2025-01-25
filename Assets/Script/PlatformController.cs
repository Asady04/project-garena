using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform character;  // Reference to the character
    public Transform[] platform;  // Array of platforms
    public float moveSpeed = 5f;  // Movement speed

    private Transform targetPlatform;  // Current platform to move towards
    private bool shouldMove = false;  // Movement control
    public int platformIndexToStart = 0;  // Starting platform index
    public bool isInputEnabled = true;
    public bool stop = false;
    public List<int> move;
    private BlueController blueController;
    public Animator animator;
    private SoundManager soundManager;

    void Start()
    {
        move = new List<int>();
        targetPlatform = platform[platformIndexToStart];
        character.position = new Vector3(targetPlatform.position.x, character.position.y, character.position.z);
        blueController = FindFirstObjectByType<BlueController>();
        animator.SetBool("isWalking", false);
        soundManager = FindFirstObjectByType<SoundManager>();
    }

    void Update()
    {
        if (shouldMove && targetPlatform != null)
        {
            // Move character towards the target platform
            character.position = Vector3.MoveTowards(
                character.position,
                new Vector3(targetPlatform.position.x, character.position.y, character.position.z),
                moveSpeed * Time.deltaTime
            );

            // Stop moving when close enough
            if (Mathf.Abs(character.position.x - targetPlatform.position.x) < 0.1f)
            {
                animator.SetBool("isWalking", false); // Stop walking animation
            }

            if (Vector3.Distance(character.position, targetPlatform.position) < 0.1f)
            {
                shouldMove = false;
            }
        }

        if (isInputEnabled)
        {
            // Input to move left or right
            if (Input.GetKeyUp(KeyCode.A))
            {
                isInputEnabled = false;
                if (!stop)
                {
                    MoveToPlatformLeft();
                    blueController.Move();
                    StartCoroutine(DelayBeforeNextInput());
                }
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                isInputEnabled = false;
                if (!stop)
                {
                    MoveToPlatformRight();
                    blueController.Move();
                    StartCoroutine(DelayBeforeNextInput());
                }
            }
        }
    }

    public void MoveToPlatformLeft()
    {
        if (platformIndexToStart > 0)
        {
            // Start walking animation
            animator.SetBool("isWalking", true);
            soundManager.Walk();

            // Prepare for movement
            move.Add(0);
            platformIndexToStart--;
            targetPlatform = platform[platformIndexToStart];
            shouldMove = true;
        }
    }

    public void MoveToPlatformRight()
    {
        if (platformIndexToStart < platform.Length - 1)
        {
            // Start walking animation
            animator.SetBool("isWalking", true);
            soundManager.Walk();

            // Prepare for movement
            move.Add(1);
            platformIndexToStart++;
            targetPlatform = platform[platformIndexToStart];
            shouldMove = true;
        }
    }

    public void AfterTP(int index)
    {
        platformIndexToStart = index;
        targetPlatform = platform[platformIndexToStart];
        shouldMove = true;
    }

    IEnumerator DelayBeforeNextInput()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(1f);

        // Re-enable input
        isInputEnabled = true;
    }
}
