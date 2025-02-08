using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private WinController winController; // Reference to the TMP_Text component


    private Queue<GameObject> displayedSteps = new Queue<GameObject>(); // Queue to manage displayed images
    public int maxStepsToDisplay = 5; // Limit for the number of steps displayed
    public Color defaultColor = Color.white; // Default color for images
    public Color highlightColor = Color.yellow; // Highlight color for the most recent step

    private GameObject lastHighlightedStep; // Reference to the last highlighted step
    void Start()
    {
        move = new List<int>();
        targetPlatform = platform[platformIndexToStart];
        character.position = new Vector3(targetPlatform.position.x, character.position.y, character.position.z);
        blueController = FindFirstObjectByType<BlueController>();
        winController = FindFirstObjectByType<WinController>();
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
        winController.GameOver();
    }
    public void Win()
    {
        soundManager.Win();
        winController.Win();
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
        // Instantiate prefab sebagai anak dari parent content
        GameObject newObject = Instantiate(prefabToInstantiate, content);

        // Reset posisi/rotasi/scale relatif ke parent
        newObject.transform.localPosition = Vector3.zero;
        newObject.transform.localRotation = Quaternion.identity;
        newObject.transform.localScale = Vector3.one;

        // Tambahkan ke queue langkah yang ditampilkan
        displayedSteps.Enqueue(newObject);

        // Jika langkah melebihi batas maksimum, hapus langkah tertua
        if (displayedSteps.Count > maxStepsToDisplay)
        {
            GameObject oldestObject = displayedSteps.Dequeue();
            Destroy(oldestObject);
        }

        // Update highlight untuk langkah ke-maxStepsToDisplay
        UpdateHighlight();
    }

    private void UpdateHighlight()
    {
        // Hanya jalankan highlight jika langkah sudah mencapai maxStepsToDisplay
        if (displayedSteps.Count >= maxStepsToDisplay)
        {
            // Reset warna untuk semua langkah
            foreach (GameObject step in displayedSteps)
            {
                Image stepImage = step.GetComponent<Image>();
                if (stepImage != null)
                {
                    stepImage.color = defaultColor; // Warna default untuk semua langkah
                }
            }

            // Highlight langkah paling belakang (urutan pertama dalam queue)
            GameObject lastStep = displayedSteps.Peek(); // Langkah paling belakang
            Image lastStepImage = lastStep.GetComponent<Image>();
            if (lastStepImage != null)
            {
                lastStepImage.color = highlightColor; // Highlight langkah terakhir
            }
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
