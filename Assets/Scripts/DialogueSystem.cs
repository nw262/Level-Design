using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueSystem : MonoBehaviour
{
    public List<DialogueLine> dialogueLines;

    private float timeCounter = 0;
    private float maxTime = 0;
    private bool dialogueIsActive;

    void Start()
    {
        // All dialogue lines should be inactive
        foreach (var dialogue in dialogueLines) {
            dialogue.gameObject.SetActive(false);
            dialogueIsActive = false;

            // Find the time it takes for all lines to finish
            maxTime += dialogue.duration;
        }
    }

    void Update()
    {
        if (dialogueIsActive)
        {
            timeCounter += Time.deltaTime;

            // If time passed equals total time, player should be able to move again
            if (timeCounter >= maxTime)
            {
                gameObject.SetActive(false);
            }
        }        
    }

    private IEnumerator OnCollisionEnter(Collision collision)
    {
        // If player, then start running dialogue
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (var dialogue in dialogueLines)
            {
                yield return StartCoroutine(StartDialogue(dialogue));
            }
        }
    }

    IEnumerator StartDialogue(DialogueLine dialogue)
    {
        // Set dialogue line to active
        dialogue.gameObject.SetActive(true);
        dialogueIsActive = true;

        // Line stays on screen for as long as its set duration
        yield return new WaitForSeconds(dialogue.duration - 0.2f);

        ResetDialogue(dialogue);
    }

    void ResetDialogue(DialogueLine dialogue)
    {
        // Sets dialogue line to inactive because it finished running
        dialogue.gameObject.SetActive(false);
    }
}
