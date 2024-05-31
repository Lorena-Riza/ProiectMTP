using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerInfection : MonoBehaviour
{
    public int currentInfection;
    public int maxInfection;
    public Slider InfectionSlider;
    public Animator animator;

    private void Awake()
    {
        maxInfection = 100;
        currentInfection = 25;
        UpdateInfectionBar();
    }

    public void TakeDamage(int amount)
    {
        currentInfection -= amount;
        UpdateInfectionBar();
        UpdateEnemyLineOfSight();

        if (currentInfection <= 0)
        {
            Die();
        }
    }

    public void Infect(int amount)
    {
        currentInfection += amount;
        currentInfection = Mathf.Clamp(currentInfection, 0, maxInfection);
        UpdateInfectionBar();
        UpdateEnemyLineOfSight();

        if (currentInfection >= maxInfection)
        {
            Die();
        }
    }

    private void UpdateInfectionBar()
    {
        if (InfectionSlider != null)
        {
            InfectionSlider.value = (float)currentInfection / maxInfection;
        }
    }

    public void Die()
    {
        animator.SetTrigger("IsDying");
        StartCoroutine(WaitBeforeRestart());
    }

    private IEnumerator WaitBeforeRestart()
    {
        yield return new WaitForSeconds(2f);

        RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateEnemyLineOfSight()
    {
        // Update all enemies line of sight
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in enemies)
        {
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.UpdateLineOfSite();
            }
        }
    }
}