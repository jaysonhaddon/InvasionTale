using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [Header("Breakable Variables")]
    [SerializeField] GameObject destroyEffect;
    [SerializeField] float deactivateTime;

    // Cached References 
    [SerializeField] private Collider2D breakableCol;
    private SpriteRenderer breakableSr;

    private void Awake()
    {
        breakableSr = GetComponent<SpriteRenderer>();
        breakableCol.enabled = true;
        breakableSr.enabled = true;
        destroyEffect.SetActive(false);
    }

    public void DeactivateObject()
    {
        StartCoroutine(DeactivateCo());
    }

    private IEnumerator DeactivateCo()
    {
        breakableCol.enabled = false;
        breakableSr.enabled = false;
        destroyEffect.SetActive(true);
        yield return new WaitForSeconds(deactivateTime);
        this.gameObject.SetActive(false);
    }
}
