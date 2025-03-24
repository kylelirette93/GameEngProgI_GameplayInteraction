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
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Color highlightColor = Color.white;

    [Header("Interactable Settings")]
    public InteractableType type;
    public ItemData itemData;
    Inventory inventory;
    GameObject interactPrompt;
    [SerializeField] string itemId;

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
        inventory = FindObjectOfType<Inventory>();
        interactPrompt = GameObject.Find("InteractPrompt");
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
                if (GameManager.Instance.questManager.quests[itemData.questIndex].isRecieved
                    && GameManager.Instance.questManager.quests[itemData.questIndex].requiredItem == itemData)
                {
                    DisplayPrompt();
                }
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

                isPickedUp[itemId] = true;
                interactPrompt.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    public void Info()
    {
        GameManager.Instance.UIManager.SetInteractionText(itemData.infoMessage);
    } 

    private void DisplayPrompt()
    {
        if (interactPrompt != null)
        {
            interactPrompt.SetActive(true);
            TextMeshProUGUI textObj = interactPrompt.GetComponent<TextMeshProUGUI>();
            textObj.text = "Press    <sprite index=0>to pickup.\r\n";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Pickup();
            }
        }
    }
}
