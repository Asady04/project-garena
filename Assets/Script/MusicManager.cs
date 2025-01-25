using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;  // Singleton instance
    private AudioSource audioSource;

    [Header("Music Clips")]
    public AudioClip mainMenuMusic;  // Music for main menu scenes
    public AudioClip inGameMusic;    // Music for in-game scenes

    [Header("Scene Settings")]
    public int[] mainMenuScenes;     // Scene indexes for main menu

    void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the current scene is a main menu scene
        if (System.Array.Exists(mainMenuScenes, index => index == scene.buildIndex))
        {
            PlayMusic(mainMenuMusic);
        }
        else
        {
            PlayMusic(inGameMusic);
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        // If the current music is different, change it
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
