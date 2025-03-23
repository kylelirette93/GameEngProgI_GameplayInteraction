using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public IInteractable interactable = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InteractObj"))
        {
            interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Highlight(true);
                interactable.Interact();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InteractObj"))
        {
            if (interactable != null)
            {
                interactable.Highlight(false);
            }
            interactable = null;
        }
    }
}
