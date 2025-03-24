using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum InteractableType
{
    Nothing,
    Pickup,
    Info
}
public class Interactable : MonoBehaviour, IInteractable
{
    [Header("Interactable Settings")]
    public InteractableType type;
    public ItemData itemData;
    Inventory inventory;
    GameObject interactPrompt;
    [SerializeField] string itemId;

    // Static dictionary to store whether an interactable has been picked up.
    static Dictionary<string, bool> isPickedUp = new Dictionary<string, bool>(); 

    public InteractableType InteractionType => type;

    
    void OnEnable()
    {
        if (isPickedUp.ContainsKey(itemId)) 
        {
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        inventory = GameManager.Instance.UIManager.inventory;
        interactPrompt = GameManager.Instance.UIManager.interactionPrompt.gameObject;
    }
    public void Interact()
    {
        switch (type)
        {
            case InteractableType.Nothing:
                Nothing();
                break;
            case InteractableType.Pickup:
                // Check if pickup is required for a quest, if so display prompt.
                if (GameManager.Instance.questManager.quests[itemData.questIndex].isRecieved
                    && GameManager.Instance.questManager.quests[itemData.questIndex].requiredItem == itemData)
                {
                    DisplayPrompt();
                }
                break;
            case InteractableType.Info:
                DisplayPrompt();
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

                isPickedUp[itemId] = true;
                interactPrompt.GetComponent<TextMeshProUGUI>().text = "";
                gameObject.SetActive(false);
            }
        }
    }

    public void Info()
    {
        // Set the floating text above player's head.
        GameManager.Instance.UIManager.SetInteractionText(itemData.infoMessage);
    }

    public void DisplayPrompt()
    {
        if (interactPrompt != null)
        {
            interactPrompt.SetActive(true);
            TextMeshProUGUI textObj = interactPrompt.GetComponent<TextMeshProUGUI>();

            textObj.text = "Press <sprite index=0> to interact.\r\n";

            //Debug.Log("DisplayPrompt called. Space key down: " + Input.GetKeyDown(KeyCode.Space));
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (type == InteractableType.Pickup)
                {
                    Pickup();
                }
                else if (type == InteractableType.Info)
                {
                    //Debug.Log("Calling Info()");
                    Info();
                }
            }
        }
    }

    public void DisablePrompt()
    {
        if (interactPrompt != null)
        {
            interactPrompt.SetActive(false);
        }
    }
}
