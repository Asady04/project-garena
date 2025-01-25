using UnityEngine;
using TMPro;

public class BlueController : MonoBehaviour
{
    public int countBeforeMoving = 0;
    int indexMoving = 0;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCol2d;
    private Animator animator;
    PlatformController platformController;
    public int platformIndex;
    private Transform targetPlatform;
    private bool shouldMove = false;
    float moveSpeed;
    public TMP_Text tmpText; // Reference to the TMP_Text component
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCol2d = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        platformController = FindFirstObjectByType<PlatformController>();
        platformIndex = platformController.platformIndexToStart;
        moveSpeed = platformController.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("blue" + platformIndex);
        if (shouldMove && targetPlatform != null)
        {
            // Move character towards the target platform
            gameObject.transform.position = Vector3.MoveTowards(
                 gameObject.transform.position,
                new Vector3(targetPlatform.position.x, gameObject.transform.position.y, gameObject.transform.position.z),
                moveSpeed * Time.deltaTime
            );

            // Stop moving when close enough
            if (Vector3.Distance(gameObject.transform.position, targetPlatform.position) < 0.1f)
            {
                shouldMove = false;
            }

        }
    }
    public void Move()
    {
        countBeforeMoving--;
        tmpText.text = countBeforeMoving.ToString();
        if (countBeforeMoving == 0)
        {
            spriteRenderer.enabled = true;
            boxCol2d.enabled = true;
            animator.enabled = true;
            rb2d.gravityScale = 1f;
        }
        if (countBeforeMoving < 0)
        {
            if (platformController.move[indexMoving] == 0)
            {
                platformIndex--;
                targetPlatform = platformController.platform[platformIndex];
                shouldMove = true;
            }
            if (platformController.move[indexMoving] == 1)
            {
                platformIndex++;
                targetPlatform = platformController.platform[platformIndex];
                shouldMove = true;
            }
            indexMoving++;
        }
    }

    public void AfterTP(int index)
    {
        platformIndex = index;
        targetPlatform = platformController.platform[platformIndex];
        shouldMove = true;
    }
}