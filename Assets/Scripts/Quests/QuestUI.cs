using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    // This class updates the UI based on active quests in the list.
    // At this time, only handles collection quests.

    public TextMeshProUGUI questListText;
    private QuestManager questManager;
    public Inventory inventory;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        UpdateQuestList();
    }

    public void UpdateQuestList()
    {
        questListText.text = "Active Quests:\n";
        if (questManager != null)
        {
            foreach (Quest quest in questManager.GetActiveQuests())
            {
                if (quest != null && quest.isRecieved)
                {
                    questListText.text += "- " + quest.title + ": " + quest.description + "\n" + "Collected: " + quest.collected + "/" + quest.condition;
                }
            }
        }
    }

    public IEnumerator DisplayCompletion(Quest completedQuest)
    {
        // Display a temporary message when quest is completed.
        gameObject.SetActive(true);

        if (inventory != null)
        {
            Debug.Log("Calling RemoveQuestItemsFromInventory");
            RemoveQuestItemsFromInventory(completedQuest);
        }

        questListText.text = $"Quest completed, you earned {completedQuest.reward} gold!";
        yield return new WaitForSeconds(3f);
        UpdateQuestList();
        gameObject.SetActive(false);
    }

    private void RemoveQuestItemsFromInventory(Quest completedQuest)
    {
        if (completedQuest != null && completedQuest.requiredItem != null)
        {
            inventory.RemoveItemsByValue(completedQuest.requiredItem, completedQuest.condition);
        }
    }
}
