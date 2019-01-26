using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInteraction : MonoBehaviour
{
    [SerializeField]
    private int initialNeonCount = 3;

    private Storm storm;

    private int currentNeonCount;

    private bool isInteracting;
    private bool canInteractAgain;

    [HideInInspector]
    public List<GameObject> interactibles;
    
    // Start is called before the first frame update
    void Start()
    {
        storm = GameObject.FindGameObjectWithTag("Storm").GetComponent<Storm>();
        interactibles = new List<GameObject>();

        currentNeonCount = initialNeonCount;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO : Remove
        if (Input.GetKeyDown("n"))
        {
            Debug.Log("Cheat code activated");
            DayNightManager.SetDay(!DayNightManager.GetDay());
        }
        
        GetComponentInChildren<InteractionCollider>().canInteract(DayNightManager.GetDay());

        if (!DayNightManager.GetDay())
        {
            return;
        }
        if (Input.GetButtonDown("Fire2") && !GetComponent<CharacterMovement>().IsJumping()
            && !storm.IsFalling())
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
                if (item.CompareTag("Rift"))
                {
                    Destroy(item);
                    GameObject.FindGameObjectWithTag("Storm").GetComponent<PictureBehaviour>().StartAnimation();
                }
            }
        }

        if (Input.GetButtonDown("Fire1") && !GetComponent<CharacterMovement>().IsJumping() 
            && storm.IsFalling())
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
           GameObject flare = Instantiate((GameObject)Resources.Load("Prefabs/UI/FlareUI"), GameObject.Find("PlayerAndCamera/UI/Day").transform);
            flare.name = "Flare" + i;
        }
    }


    void AddLast()
    {
        Image[] flares = GameObject.Find("PlayerAndCamera/UI/Day").GetComponentsInChildren<Image>();
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
        Image[] flares =  GameObject.Find("PlayerAndCamera/UI/Day").GetComponentsInChildren<Image>();
      
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
