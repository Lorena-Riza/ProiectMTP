using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite crossbowSprite;
    public Sprite copperGunSprite;
    public Sprite copperBulletsSprite;
    public Sprite tunaCanSprite;
    public Sprite sandwichSprite;
    public Sprite chipsSprite;
    public Sprite burittoSprite;
}
