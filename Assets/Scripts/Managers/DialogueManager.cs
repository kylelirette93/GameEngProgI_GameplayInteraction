using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public string[] DialogueLines;
    public TextMeshProUGUI dialogueText;
    private Queue<string> dialogue;

    private void Start()
    {
        dialogue = new Queue<string>();
    }
    /*public void DisplayDialogue()
    {
        if (dialogue != null && dialogue.Count > 0)
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(DisplayDialogueCoroutine());
        }
        if (DialogueLines != null && DialogueLines.Length > 0)
        {
            dialoguePanel.SetActive(true);
            // Debug.Log("Activating dialogue panel");
            StartCoroutine(DisplayDialogueCoroutine());
        }
    }*/

    public void StartDialogue(string[] sentences)
    {
        dialogue.Clear();
        dialoguePanel.SetActive(true);
        // Queue the dialogue.
        foreach (string currentString in sentences)
        {          
             dialogue.Enqueue(currentString);     
        }
       
        DisplayNextSentence();        
    }

    public void DisplayNextSentence()
    {
        if (dialogue.Count == 0) EndSentence();

        // Debug.Log("Button being clicked");
        if (dialogue.Count > 0)
        {
            string currentSentence = dialogue.Dequeue();
            StartCoroutine(DisplayDialogueCoroutine(currentSentence));
        }
    }

    public void EndSentence()
    {
        dialogue.Clear();
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
    }
    private IEnumerator DisplayDialogueCoroutine(string currentSentence)
    {
            Time.timeScale = 0f;        
            foreach (char letter in currentSentence)
            {
                // Iterate through each character in each line of dialogue for typewriter effect.
                GameManager.Instance.SoundManager.PlaySoundByName("typewriter");
                dialogueText.text += letter;
                // Wait for real time so that this still happens when time scale is 0.
                yield return new WaitForSecondsRealtime(0.05f);
            }
            yield return new WaitForSecondsRealtime(1f);

            Time.timeScale = 1f;
    }
}
