
public interface IInteractable
{
    void Interact();
    void Highlight(bool enable);
    InteractableType InteractionType { get; }
}
