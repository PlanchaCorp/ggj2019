using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    [SerializeField]
    private int initialNeonCount = 3;

    private int currentNeonCount;

    private bool isInteracting;
    private bool canInteractAgain;

    public List<GameObject> interactibles;





    // Start is called before the first frame update
    void Start()
    {

        interactibles = new List<GameObject>();

        currentNeonCount = initialNeonCount;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("n"))
        {
            DayNightManager.SetDay(false);
            DayNightManager.ChangeCycle();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("interaction");
            Queue<GameObject> items = new Queue<GameObject>(interactibles);
            if (items.Count > 0)
            {
                GameObject item = items.Dequeue();
                if (item.CompareTag("Neon"))
                    {
                        RemoveNeon(item);
                    }
            }
           
           

        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (currentNeonCount > 0)
            {
                PlaceNeon();
            }
        }

    }

    void RemoveNeon(GameObject neon)
    {
        currentNeonCount++;
        Destroy(neon);
        DayNightManager.neons = GameObject.FindGameObjectsWithTag("Neon");
    }


    void PlaceNeon()
    {
        currentNeonCount--;
        GameObject neon = Instantiate((GameObject)Resources.Load("Prefabs/Neon"));
        neon.transform.position = transform.position;
        DayNightManager.neons = GameObject.FindGameObjectsWithTag("Neon");
    }




}
