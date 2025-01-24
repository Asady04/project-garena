using System;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform character;  // Reference to the character
    public Transform[] platform;  // Array of platforms
    public float moveSpeed = 5f;  // Movement speed

    private Transform targetPlatform;  // Current platform to move towards
    private bool shouldMove = false;  // Movement control
    public int platformIndexToStart = 0;  // Starting platform index

    void Start()
    {
        targetPlatform = platform[platformIndexToStart];
        character.position = new Vector3(targetPlatform.position.x, character.position.y, character.position.z);
    }

    void Update()
    {
        Debug.Log(platformIndexToStart);
        if (shouldMove && targetPlatform != null)
        {
            // Move character towards the target platform
            character.position = Vector3.MoveTowards(
                character.position,
                new Vector3(targetPlatform.position.x, character.position.y, character.position.z),
                moveSpeed * Time.deltaTime
            );

            // Stop moving when close enough
            if (Vector3.Distance(character.position, targetPlatform.position) < 0.1f)
            {
                shouldMove = false;
            }
        }

        // Input to move left or right
        if (Input.GetKeyUp(KeyCode.A))
        {
            MoveToPlatformLeft();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            MoveToPlatformRight();
        }
    }

    public void MoveToPlatformLeft()
    {
        if (platformIndexToStart > 0)
        {
            platformIndexToStart--;
            targetPlatform = platform[platformIndexToStart];
            shouldMove = true;
        }
    }

    public void AfterTP(int index){
        platformIndexToStart = index;
        targetPlatform = platform[platformIndexToStart];
        shouldMove = true;
    }
    public void MoveToPlatformRight()
    {
        if (platformIndexToStart < platform.Length - 1)
        {
            platformIndexToStart++;
            targetPlatform = platform[platformIndexToStart];
            shouldMove = true;
        }
    }
}
