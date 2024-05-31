using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public Transform teleportDestination; // Reference to the destination where the player will be teleported
    public AudioClip newBackgroundMusic; // New background music audio clip
    public GameObject backgroundMusicObject; // Reference to the background music game object

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = teleportDestination.position;

            if (newBackgroundMusic != null && backgroundMusicObject != null)
            {
                AudioSource audioSource = backgroundMusicObject.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.clip = newBackgroundMusic;
                    audioSource.Play();
                }
            }
        }
    }
}