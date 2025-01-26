using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public GameObject leftArrow;
    public GameObject rightArrow;
    public Transform content;      // The parent where the prefab should be instantiated
    public int stepLimit;
    public TMP_Text tmpText; // Reference to the TMP_Text component

    void Start()
    {
        move = new List<int>();
        targetPlatform = platform[platformIndexToStart];
        character.position = new Vector3(targetPlatform.position.x, character.position.y, character.position.z);
        blueController = FindFirstObjectByType<BlueController>();
        animator.SetBool("isWalking", false);
        soundManager = FindFirstObjectByType<SoundManager>();
        tmpText.text = stepLimit.ToString();
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
            if (stepLimit == 0)
            {
                GameOver();
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

    public void GameOver()
    {
        soundManager.GameOver();
    }
    public void MoveToPlatformLeft()
    {
        if (platformIndexToStart > 0)
        {

            // Start walking animation
            animator.SetBool("isWalking", true);
            soundManager.Walk();
            InstantiatePrefab(leftArrow);

            // Prepare for movement
            move.Add(0);
            platformIndexToStart--;
            targetPlatform = platform[platformIndexToStart];
            shouldMove = true;
            stepLimit--;
            tmpText.text = stepLimit.ToString();
        }
    }

    public void MoveToPlatformRight()
    {
        if (platformIndexToStart < platform.Length - 1)
        {
            // Start walking animation
            animator.SetBool("isWalking", true);
            soundManager.Walk();
            InstantiatePrefab(rightArrow);

            // Prepare for movement
            move.Add(1);
            platformIndexToStart++;
            targetPlatform = platform[platformIndexToStart];
            shouldMove = true;
            stepLimit--;
            tmpText.text = stepLimit.ToString();
        }
    }

    public void InstantiatePrefab(GameObject prefabToInstantiate)
    {
        // Instantiate the prefab as a child of the parent transform
        GameObject newObject = Instantiate(prefabToInstantiate, content);

        // Optionally, reset the local position/rotation/scale
        newObject.transform.localPosition = Vector3.zero; // Position relative to the parent
        newObject.transform.localRotation = Quaternion.identity; // Reset rotation
        newObject.transform.localScale = Vector3.one; // Reset scale
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
