using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the PauseMenu panel
    public GameObject settingsMenu; // Reference to the settings menu panel
    [SerializeField] private AudioClip buttonClickSound;

    void Start()
    {
        if (pauseMenu == null)
        {
            Debug.LogError("Pause menu is not assigned in the inspector!");
        }

        if (settingsMenu == null)
        {
            Debug.LogError("Settings menu is not assigned in the inspector!");
        }
    }

    public void BackToPauseMenu()
    {
        SoundManager.instance.PlaySoundFX(buttonClickSound, transform.position, 1f);

        if (settingsMenu != null)
        {
            settingsMenu.SetActive(false);
        }

        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
    }
}