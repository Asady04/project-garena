using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isMuted = false;

    [Header("Sound Effects")]
    public AudioClip walk; // Assign your button click sound here
    public AudioClip button; // Assign your button click sound here
    public AudioClip gameOver; // Assign your button click sound here
    public AudioClip collide; // Assign your button click sound here
    public AudioClip portal; // Assign your button click sound here
    public AudioClip startButton; // Assign your button click sound here
    public AudioClip switchSound; // Assign your button click sound here
    public AudioClip trampoline; // Assign your button click sound here
    public AudioClip win; // Assign your button click sound here

    void Awake()
    {
        // Make sure there's only one SoundManager in the scene (Singleton pattern)
        if (FindObjectsByType<SoundManager>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // Persist across scenes
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        audioSource.mute = isMuted;
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        audioSource.mute = isMuted;
        PlayerPrefs.SetInt("MusicMuted", isMuted ? 1 : 0);
    }
    public void Walk()
    {
        if (walk != null)
        {
            audioSource.PlayOneShot(walk);
        }
    }
    public void ButtonClick()
    {
        if (button != null)
        {
            audioSource.PlayOneShot(button);
        }
    }
    public void GameOver()
    {
        if (gameOver != null)
        {
            audioSource.PlayOneShot(gameOver, Mathf.Clamp01(1.5f));
        }
    }
    public void Collide()
    {
        if (collide != null)
        {
            audioSource.PlayOneShot(collide);
        }
    }
    public void Portal()
    {
        if (portal != null)
        {
            audioSource.PlayOneShot(portal);
        }
    }
    public void SwitchSound()
    {
        if (switchSound != null)
        {
            audioSource.PlayOneShot(switchSound);
        }
    }
    public void StartButton()
    {
        if (startButton != null)
        {
            audioSource.PlayOneShot(startButton);
        }
    }
    public void Trampoline()
    {
        if (trampoline != null)
        {
            audioSource.PlayOneShot(trampoline);
        }
    }
    public void Win()
    {
        if (win != null)
        {
            audioSource.PlayOneShot(win);
        }
    }
}
