using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPost : Interactable
{
    [Header("Sign Post Variables")]
    [SerializeField] private string signDialogue;

    public override void PerformInteraction()
    {
        Debug.Log(signDialogue);
        base.PerformInteraction();
    }
}
