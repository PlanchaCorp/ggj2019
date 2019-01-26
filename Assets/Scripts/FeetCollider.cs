using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollider : MonoBehaviour
{
    private CharacterMovement characterMovement;
    private int isOverHole;

    private void Start()
    {
        isOverHole = 0;
        characterMovement = transform.parent.gameObject.GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if (isOverHole > 0 && !characterMovement.IsJumping())
        {
            GameObject.FindGameObjectWithTag("Storm").GetComponent<Storm>().TriggerFallGameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole"))
        {
            isOverHole++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole"))
        {
            isOverHole--;
        }
    }
}
