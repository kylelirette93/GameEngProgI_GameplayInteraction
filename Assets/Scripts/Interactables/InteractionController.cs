using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public IInteractable interactable = null;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("InteractObj"))
        {
            interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    private void Update()
    {
        if (interactable != null)
        {
            interactable.Interact();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InteractObj"))
        {
            interactable = null;
        }
    }
}
