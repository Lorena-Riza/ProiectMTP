using UnityEngine;
using UnityEngine.UI;

public class Infection : MonoBehaviour
{
    public PlayerInfection playerInfection;
    public Image fillImage;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        float fillValue = (float)playerInfection.currentInfection / playerInfection.maxInfection;
        slider.value = fillValue;
    }
}
