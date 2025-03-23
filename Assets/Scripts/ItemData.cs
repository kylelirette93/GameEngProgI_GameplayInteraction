using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    public Sprite itemIcon; // Store the item icon
    public string itemName; //store the item name
    public string infoMessage; //store info message
    public int questIndex; //store quest index.
    // Add other relevant data...
}