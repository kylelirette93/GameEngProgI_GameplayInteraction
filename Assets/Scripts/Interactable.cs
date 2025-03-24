using System.Collections;
using UnityEngine;

public enum InteractableType
{
    Nothing,
    Pickup,
    Info
}
public class Interactable : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Color highlightColor = Color.white;

    [Header("Interactable Settings")]
    public InteractableType type;
    public ItemData itemData;
    Inventory inventory;

    public InteractableType InteractionType => type;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }
    public void Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);

        switch (type)
        {
            case InteractableType.Nothing:
                Nothing();
                break;
            case InteractableType.Pickup:
                Pickup();
                break;
            case InteractableType.Info:
                Info();
                break;
            default:
                Nothing();
                break;
        }    
    }

    public void Nothing()
    {
        Debug.Log("Interaction type not defined.");
    }

    public void Pickup()
    {
        if (inventory != null)
        {
            if (GameManager.Instance.questManager.quests[itemData.questIndex].isRecieved)
            {
                inventory.AddItem(itemData);
                GameManager.Instance.questManager.quests[itemData.questIndex].collected += 1;
                GameManager.Instance.questManager.QuestUI.UpdateQuestList();
                gameObject.SetActive(false);
            }
        }
    }

    public void Info()
    {
        GameManager.Instance.UIManager.SetInteractionText(itemData.infoMessage);
    } 
}
