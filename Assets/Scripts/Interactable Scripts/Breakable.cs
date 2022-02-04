using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [Header("Breakable Variables")]
    [SerializeField] GameObject destroyEffect;

    // Cached References
    private SpriteRenderer breakableSr;
    private Collider2D breakableCol;

    private void Awake()
    {
        breakableSr = GetComponent<SpriteRenderer>();
        breakableCol = GetComponent<Collider2D>();
        breakableCol.enabled = true;
        breakableSr.enabled = true;
        destroyEffect.SetActive(false);
    }

    public IEnumerator DeactivateObject()
    {
        breakableCol.enabled = false;
        breakableSr.enabled = false;
        destroyEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}
