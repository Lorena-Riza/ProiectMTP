using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNPCInteraction : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer playerSpriteRenderer;
    public SpriteRenderer npcSpriteRenderer;

    private void Update()
    {
        // Check if player is above or below the npc
        if (player.transform.position.y > transform.position.y)
        {
            playerSpriteRenderer.sortingOrder = npcSpriteRenderer.sortingOrder - 1;
        }
        else
        {
            playerSpriteRenderer.sortingOrder = npcSpriteRenderer.sortingOrder + 1;
        }
    }
}
