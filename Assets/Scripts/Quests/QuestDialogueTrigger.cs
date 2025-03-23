using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDialogueTrigger : MonoBehaviour
{
    // Dialogue to pass to dialogue manager when triggered.
    public string[] questDialogue;

    // Title of the quest, to match with the quest manager.
    public string Title;

    private bool hasTriggered = false;

    // Will be rewritten in the future to make more modular.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Match the quest by title.
            Quest quest = GameManager.Instance.questManager.quests.Find(q => q.title == Title);

            // Check if quest has already triggered, if not display dialogue.
            if (quest != null && !hasTriggered)
            {
                GameManager.Instance.dialogueManager.DialogueLines = questDialogue;
                hasTriggered = true;
                if (GameManager.Instance.dialogueManager.DialogueLines.Length > 0)
                {
                    GameManager.Instance.dialogueManager.DisplayDialogue();
                    GameManager.Instance.dialogueManager.DialogueLines = new string[0];
                }
            }       
        }
    }
}
