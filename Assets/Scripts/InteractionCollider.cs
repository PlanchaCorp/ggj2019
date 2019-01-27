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
        if (collision.CompareTag("Neon") || collision.CompareTag("Door") || collision.CompareTag("Rift"))
        {
            
            switch (collision.tag)
            {
                case "Neon":
                    interactionCanvas.transform.Find("Frame").GetComponentInChildren<TextMeshProUGUI>().text = "Pickup";
                    break;
                case "Door":
                    interactionCanvas.transform.Find("Frame").GetComponentInChildren<TextMeshProUGUI>().text = "Open";
                    break;
                case "Rift":
                    interactionCanvas.transform.Find("Frame").GetComponentInChildren<TextMeshProUGUI>().text = "Remember";
                    break;
                default:
                    interactionCanvas.transform.Find("Frame").GetComponentInChildren<TextMeshProUGUI>().text = collision.name;
                    break;
            }
            
           
            interactionCanvas.transform.Find("Frame").gameObject.SetActive(true);
            characterInteraction.interactibles.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Neon") || collision.CompareTag("Door") || collision.CompareTag("Rift"))
        {
            interactionCanvas.transform.Find("Frame").gameObject.SetActive(false);
            characterInteraction.interactibles.Remove(collision.gameObject);
        }
       
    }
}
