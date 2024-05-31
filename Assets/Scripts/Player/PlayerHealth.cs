using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public Slider healthSlider;
    public Animator animator;

    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip damageSound;

    private void Awake()
    {
        maxHealth = 100; 
        currentHealth = maxHealth; 
        UpdateHealthBar();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        SoundManager.instance.PlaySoundFX(damageSound, transform.position, 1f);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar(); 

        if (currentHealth == 0)
        {
            Die(); 
        }
    }

    private void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }

    public void Die()
    {
        SoundManager.instance.PlaySoundFX(deathSound, transform.position, 1f);
        Debug.Log("Die() method called.");

        // Set "IsDying" trigger
        animator.SetTrigger("IsDying");
        StartCoroutine(WaitBeforeRestart());
    }

    IEnumerator WaitBeforeRestart()
    {
        yield return new WaitForSeconds(2f);

        // Restart level
        RestartLevel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}