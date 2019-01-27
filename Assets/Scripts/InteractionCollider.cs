using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionCollider : MonoBehaviour
{

    CharacterInteraction characterInteraction;
    Canvas interactionCanvas;

    private void Start()
    {
        characterInteraction = GetComponentInParent<CharacterInteraction>();
        interactionCanvas = GameObject.Find("InteractionCanvas").GetComponent<Canvas>();
    }

    public void canInteract(bool canInteract)
    {
        GetComponent<CircleCollider2D>().enabled = canInteract;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Neon") || collision.CompareTag("Door") || collision.CompareTag("Rift") || collision.CompareTag("Home"))
        {
            interactionCanvas.transform.Find("Frame").GetComponentInChildren<TextMeshProUGUI>().text = collision.tag;
            interactionCanvas.transform.Find("Frame").gameObject.SetActive(true);
            characterInteraction.interactibles.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Neon") || collision.CompareTag("Door") || collision.CompareTag("Rift") || collision.CompareTag("Home"))
        {
            interactionCanvas.transform.Find("Frame").gameObject.SetActive(false);
            characterInteraction.interactibles.Remove(collision.gameObject);
        }
       
    }
}
