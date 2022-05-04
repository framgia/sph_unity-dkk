using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    private bool Toggle = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Toggle == false)
            {
                Pause();
            }
            else if (Toggle == true)
            {
                Resume();
            }
        }
    }

    private void Pause()
    {
        PausePanel.SetActive(true);
        Toggle = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Toggle = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
