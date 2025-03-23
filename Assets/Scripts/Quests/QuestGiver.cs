using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public string questTitle;

    // When triggered, have the player recieve the quest, also turn in if completed.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            QuestManager questManager = FindObjectOfType<QuestManager>();
            questManager.CompleteQuest(questTitle);
            foreach (var quest in questManager.quests)
            {
                if (!quest.isRecieved)
                {
                    quest.isRecieved = true;
                    GameManager.Instance.questManager.QuestUI.UpdateQuestList();
                }
            }
        }
    }
}