using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactable Variables")]
    [SerializeField] public PlayerMaster player;
    [SerializeField] public string[] interactTags;
    [SerializeField] public string newSortingLayer;

    // Inspector References
    [SerializeField] public SpriteRenderer interactableSr;

    // Base method used for Interact
    public virtual void PerformInteraction()
    {
        player.CurrentState = PlayerState.normal;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(interactTags[0]))
        {
            player = other.GetComponent<PlayerMaster>();
            if (!player.holdingObject) 
            {
                AllowPlayerInteraction();
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!player.holdingObject) 
            {
                DisablePlayerInteraction();
            }
        }
    }

    public void AllowPlayerInteraction() 
    {
        player.currentInteractable = this;
        player.canInteract = true;
    }

    public void DisablePlayerInteraction() 
    {
        player.canInteract = false;
        player.currentInteractable = null;
        player = null;
    }

    // Called by the PlayerMaster script through the Animator, timing changes based on facing position of the player
    public void ChangeSortingLayer()
    {
        interactableSr.sortingLayerName = newSortingLayer;
    }
}
