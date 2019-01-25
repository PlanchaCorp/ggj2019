using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObjects : MonoBehaviour {
    [SerializeField]
    private int initialNeonCount = 3;

    private int currentNeonCount;

    void Start()
    {
        currentNeonCount = initialNeonCount;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentNeonCount > 0)
            {
                PlaceNeon();
            }
        }
    }

    void PlaceNeon()
    {
        currentNeonCount--;
        Instantiate(Resources.Load("Prefabs/Neon"));
    }
}
