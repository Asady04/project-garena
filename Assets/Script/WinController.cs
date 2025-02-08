using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class WinController : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    // Update is called once per frame
    public void Win()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        // Get the current active scene's build index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Check if there's a next scene in the build settings
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
