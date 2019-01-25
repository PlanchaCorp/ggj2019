using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCollider : MonoBehaviour
{

    CharacterInteraction characterInteraction;

    private void Start()
    {
        characterInteraction = GetComponentInParent<CharacterInteraction>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Neon"))
        {
            characterInteraction.interactibles.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Neon"))
        {
            characterInteraction.interactibles.Remove(collision.gameObject);
        }
       
    }
}
