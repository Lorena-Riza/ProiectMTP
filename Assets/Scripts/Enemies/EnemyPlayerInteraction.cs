using UnityEngine;

public class EnemyPlayerInteraction : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer playerSpriteRenderer;
    public SpriteRenderer enemySpriteRenderer;

    private void Update()
    {
        if (player.transform.position.y > transform.position.y)
        {
            playerSpriteRenderer.sortingOrder = enemySpriteRenderer.sortingOrder - 1;
        }
        else
        {
            playerSpriteRenderer.sortingOrder = enemySpriteRenderer.sortingOrder + 1;
        }
    }
}
