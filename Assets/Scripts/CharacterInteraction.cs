using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    private CircleCollider2D interactionField;

    private bool isInteracting;
    // Start is called before the first frame update
    void Start()
    {
        interactionField = GetComponentInChildren<CircleCollider2D>();

        interactionField.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("w") && !isInteracting)
        {
            isInteracting = true;
        }
        if( isInteracting)
        {
            interactionField.enabled = true;
          
        }
        if (Input.GetButtonUp("w"))
        {
            isInteracting = false;
        }

    }

}
