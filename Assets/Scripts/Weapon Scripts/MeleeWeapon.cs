using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{

    [Header("Melee Weapon Variables")]
    [SerializeField] private string damageTag;
    [SerializeField] GameObject attackEffect;

    // Cached References
    private Collider2D weaponCollider;


    // Start is called before the first frame update
    void Start()
    {
        weaponCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(damageTag))
        {
            other.gameObject.SetActive(false);
            
            // CHANGE THIS!!!!
            // Need to either have this effect attached to the damaged object, or pull the effect in from a pool
            Instantiate(attackEffect, other.transform.position, Quaternion.identity);
        }
    }
}
