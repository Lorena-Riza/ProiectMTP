using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundFX(AudioClip clip, Vector3 position, float volume)
    {
        // Spawn the game object
        AudioSource audioSource = Instantiate(soundFXObject, position, Quaternion.identity);

        // Assign the clip to the audio source
        audioSource.clip = clip;

        // Assign the volume to the audio source
        audioSource.volume = volume;

        // Play the audio source
        audioSource.Play();

        // Get the length of the clip
        float clipLength = audioSource.clip.length;

        // Destroy the game object after the clip length
        Destroy(audioSource.gameObject, clipLength);
    }
}