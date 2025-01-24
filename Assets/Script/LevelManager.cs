using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int holdlvl;

    void Start()
    {
        // currentlvl = PlayerPrefs.GetInt("Level", 1);
        holdlvl = 15;
    }
}
