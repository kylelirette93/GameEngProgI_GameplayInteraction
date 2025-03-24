
public interface IInteractable
{
    void Interact();
    void DisablePrompt();
    InteractableType InteractionType { get; }
}
