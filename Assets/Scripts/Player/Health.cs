using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image fillImage;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        float fillValue = (float)playerHealth.currentHealth / playerHealth.maxHealth;
        slider.value = fillValue;
    }
}
