using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactable Variables")]
    [SerializeField] public PlayerMaster player;

    // Base method used for Interact
    public virtual void PerformInteraction()
    {
        player.CurrentState = PlayerState.normal;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerMaster>();
            player.currentInteractable = this;
            player.canInteract = true;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.canInteract = false;
            player.currentInteractable = null;
            player = null;
        }
    }
}
