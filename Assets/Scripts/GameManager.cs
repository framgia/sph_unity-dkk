using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int level;
    public int lives;
    public int score;

    private int firstLevel = 1;

    private void Start() 
    {
        DontDestroyOnLoad(gameObject);
    }

    public void NewGame()
    {
        lives = 3;
        score = 0;
        
        LoadLevel(firstLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadLevel(int index) 
    {
        level = index;

        Camera camera = Camera.main;

        if (camera != null) {
            camera.cullingMask = 0;
        }
        
        Invoke(nameof(LoadScene), 1f);
    }

    private void LoadScene() 
    {
        SceneManager.LoadScene(level);
    }

    public void LevelComplete() 
    {
        score += 1000;

        int nextLevel = level + 1;
        
        if (nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            LoadLevel(nextLevel);
        }
        else
        {
            LoadLevel(firstLevel);
        }
    }

    public void LevelFailed()
    {
        lives--;

        if (lives <= 0) 
        {
            NewGame();
        }
        else
        {
            LoadLevel(level);
        }
    }
}
