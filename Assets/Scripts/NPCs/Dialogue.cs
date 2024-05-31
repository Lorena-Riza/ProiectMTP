using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int dialogueIndex = 0;

    public float wordSpeed;
    public bool playerIsClose = false;


    void Start()
    {
        dialogueText.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (!dialogueBox.activeInHierarchy)
            {
                dialogueBox.SetActive(true);
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[dialogueIndex])
            {
                NextLine();
            }

        }
        if (Input.GetKeyDown(KeyCode.Q) && dialogueBox.activeInHierarchy) // Change to KeyCode.Q
        {
            RemoveText();
        }
    }

    public void RemoveText()
    {
        dialogueText.text = "";
        dialogueIndex = 0;
        dialogueBox.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[dialogueIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (dialogueIndex < dialogue.Length - 1)
        {
            dialogueIndex++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            RemoveText();
        }
    }
}