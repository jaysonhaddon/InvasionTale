using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{

    [Header("Melee Weapon Variables")]
    [SerializeField] private string[] damageTags;
    [SerializeField] GameObject attackEffect;

    // Cached References


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(damageTags[0]))
        {
            Breakable breakableObject = other.GetComponent<Breakable>();
            breakableObject.StartCoroutine(breakableObject.DeactivateObject());
        }
    }
}
