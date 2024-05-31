using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClickSound;
    private float soundDelay = 0.5f;

    public void PlayGame()
    {
        StartCoroutine(PlaySoundAndChangeScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void GoToSettings()
    {
        StartCoroutine(PlaySoundAndChangeScene("Settings_Menu"));
    }

    public void GoToMainMenu()
    {
        StartCoroutine(PlaySoundAndChangeScene("Main_Menu"));
    }

    public void QuitGame()
    {
        StartCoroutine(PlaySoundAndQuit());
    }

    private IEnumerator PlaySoundAndChangeScene(int sceneIndex)
    {
        SoundManager.instance.PlaySoundFX(buttonClickSound, transform.position, 1f);
        yield return new WaitForSeconds(soundDelay);
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator PlaySoundAndChangeScene(string sceneName)
    {
        SoundManager.instance.PlaySoundFX(buttonClickSound, transform.position, 1f);
        yield return new WaitForSeconds(soundDelay);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator PlaySoundAndQuit()
    {
        SoundManager.instance.PlaySoundFX(buttonClickSound, transform.position, 1f);
        yield return new WaitForSeconds(soundDelay);
        Application.Quit();
    }
}