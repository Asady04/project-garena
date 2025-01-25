using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class PlatformController : MonoBehaviour
{
    public Transform character;  // Reference to the character
    public Transform[] platform;  // Array of platforms
    public float moveSpeed = 5f;  // Movement speed

    private Transform targetPlatform;  // Current platform to move towards
    private bool shouldMove = false;  // Movement control
    public int platformIndexToStart = 0;  // Starting platform index
    public bool isInputEnabled = true;
    public List<int> move;  
    BlueController blueController;
    

    void Start()
    {
        move = new List<int>{};
        targetPlatform = platform[platformIndexToStart];
        character.position = new Vector3(targetPlatform.position.x, character.position.y, character.position.z);
        blueController = FindFirstObjectByType<BlueController>();
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

        if (isInputEnabled)
        {
            // Input to move left or right
            if (Input.GetKeyUp(KeyCode.A))
            {
                isInputEnabled = false;
                MoveToPlatformLeft();
                blueController.Move();
                StartCoroutine(DelayBeforeNextInput());
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                isInputEnabled = false;
                MoveToPlatformRight();
                blueController.Move();
                StartCoroutine(DelayBeforeNextInput());
            }
        }

    }

    public void MoveToPlatformLeft()
    {
        if (platformIndexToStart > 0)
        {
            move.Add(0);
            platformIndexToStart--;
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
    public void MoveToPlatformRight()
    {
        if (platformIndexToStart < platform.Length - 1)
        {
            move.Add(1);
            platformIndexToStart++;
            targetPlatform = platform[platformIndexToStart];
            shouldMove = true;
        }
    }

    IEnumerator DelayBeforeNextInput()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(1f);

        // Call the method you want to execute after the delay
        isInputEnabled = true;
    }
}