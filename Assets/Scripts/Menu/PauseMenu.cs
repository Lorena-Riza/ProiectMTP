using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public static bool isPaused;

    [SerializeField] private AudioClip buttonClickSound;

    void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            Debug.LogError("Pause menu is not assigned in the inspector!");
        }

        if (settingsMenu != null)
        {
            settingsMenu.SetActive(false);
        }
        else
        {
            Debug.LogError("Settings menu is not assigned in the inspector!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
        isPaused = true;
    }

    public void ResumeGame()
    {
        SoundManager.instance.PlaySoundFX(buttonClickSound, transform.position, 1f);
        Time.timeScale = 1f;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(false);
        }
        isPaused = false;
    }

    public void ShowSettingsMenu()
    {
        SoundManager.instance.PlaySoundFX(buttonClickSound, transform.position, 1f);
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(true);
        }
    }

    public void HideSettingsMenu()
    {
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(false);
        }
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
        PauseMenu.isPaused = true; // Ensure the game is still paused
    }

    public void GoToMainMenu()
    {
        SoundManager.instance.PlaySoundFX(buttonClickSound, transform.position, 1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu");
    }

    public void RestartGame()
    {
        SoundManager.instance.PlaySoundFX(buttonClickSound, transform.position, 1f);
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}