using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("n"))
        {
            DayNightManager.SetDay(!DayNightManager.GetDay());
            DayNightManager.ChangeCycle();
        }

    
        GetComponentInChildren<InteractionCollider>().canInteract(DayNightManager.GetDay());
        
        if (Input.GetButtonDown("Fire2"))
        {
           
            Queue<GameObject> items = new Queue<GameObject>(interactibles);
            if (items.Count > 0)
            {
                GameObject item = items.Dequeue();
                if (item.CompareTag("Neon"))
                    {
                        RemoveNeon(item);
                    }
                if (item.CompareTag("Door"))
                {
                  
                    item.GetComponentInParent<DoorManager>().Open();
                    interactibles.Remove(item);
                    GameObject.Find("Frame").gameObject.SetActive(false);
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
        AddLast();
    }


    void PlaceNeon()
    {
        currentNeonCount--;
        GameObject neon = Instantiate((GameObject)Resources.Load("Prefabs/Neon"));
        neon.transform.position = transform.position;
        DayNightManager.neons = GameObject.FindGameObjectsWithTag("Neon");
        RemoveLast();



    }
    void UpdateUI()
    {

        for(int i = 0; i < currentNeonCount; i++)
        {
           GameObject flare = Instantiate((GameObject)Resources.Load("Prefabs/UI/FlareUI"), GameObject.Find("PlayerAndCamera/UI/Flares").transform);
            flare.name = "Flare" + i;
        }
    }


    void AddLast()
    {
        Image[] flares = GameObject.Find("PlayerAndCamera/UI/Flares").GetComponentsInChildren<Image>();
        Debug.Log(flares.Length);
        Array.Reverse(flares);
        foreach (Image flare in flares)
        {
            if (!flare.enabled)
            {
                flare.enabled = true;
                return;
            }
        }
    }
    void RemoveLast()
    {
        Image[] flares =  GameObject.Find("PlayerAndCamera/UI/Flares").GetComponentsInChildren<Image>();
      
        foreach (Image flare in flares)
        {
            if (flare.enabled)
            {
                flare.enabled = false;
                return;
            }
        }
    }




}
