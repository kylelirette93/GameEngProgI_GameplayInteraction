using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // List of quest objects.
    public List<Quest> quests = new List<Quest>();
    public QuestUI QuestUI;


    public void CompleteQuest(string title)
    {
        // Search the list of quests, match the title and complete the quest.
        Quest quest = quests.Find(q => q.title == title);
        if (quest != null && !quest.isCompleted)
        {
            quest.CheckCompletion();
            // Temporarily display the quest is completed, for feedback.
            if (QuestUI != null && quest.isCompleted)
            {
                StartCoroutine(QuestUI.DisplayCompletion(quest));
                // Remove the completed quest.
                quests.Remove(quest);
            }
            }
        }

    public List<Quest> GetActiveQuests()
    {
        // Search through and return all quests that are not completed.
        return quests.FindAll(q => !q.isCompleted);
    }

}
