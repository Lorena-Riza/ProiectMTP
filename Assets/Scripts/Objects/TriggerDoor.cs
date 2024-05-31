using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    public string tagTarget = "Player";
    public List<Collider2D> detectedObjects = new List<Collider2D>();

    Animator animator;
    public string doorOpenAnimationName = "Door_Open";
    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag==tagTarget)
        {
            detectedObjects.Add(collider);
            if (detectedObjects.Count > 0)
            {
                animator.SetBool(doorOpenAnimationName, true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagTarget)
        {
            detectedObjects.Remove(collider);
            if (detectedObjects.Count == 0)
            {
                animator.SetBool(doorOpenAnimationName, false);
            }
        }
    }
}
