using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int holdlvl;
    //default 1, assign on object not in script, temp only before using prefs

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
