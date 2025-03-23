using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flower : MonoBehaviour
{
    // Quest object for Violet Essence.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Pick up flower, update quest list and destroy the flower.
        GameManager.Instance.questManager.quests[0].collected += 1;
        GameManager.Instance.questManager.QuestUI.UpdateQuestList();
        Destroy(gameObject);
    }


}
