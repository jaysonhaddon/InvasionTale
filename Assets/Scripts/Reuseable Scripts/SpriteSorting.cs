using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorting : MonoBehaviour
{
    [Header("Sprite Sorting Variables")]
    [SerializeField] string defaultSortingLayer;
    [SerializeField] string newSortingLayer;
    [SerializeField] string triggerTag;

    //Cached References
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = defaultSortingLayer;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag(triggerTag)) 
        {
            spriteRenderer.sortingLayerName = newSortingLayer;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        spriteRenderer.sortingLayerName = defaultSortingLayer;
    }
}
