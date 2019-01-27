using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterInteraction : MonoBehaviour
{
    [SerializeField]
    private int initialNeonCount = 3;
    private float WINTRANSITIONTIME = 1;

    private Storm storm;
    private Image backgroundImage;

    private int currentNeonCount;
    private float winTransition;

    private bool isInteracting;
    private bool canInteractAgain;

    private bool hasWon;

    [HideInInspector]
    public List<GameObject> interactibles;
    
    // Start is called before the first frame update
    void Start()
    {
        hasWon = false;
        storm = GameObject.FindGameObjectWithTag("Storm").GetComponent<Storm>();
        backgroundImage = storm.transform.Find("GameOverCanvas").Find("GameOverBackground").GetComponent<Image>();
        interactibles = new List<GameObject>();

        currentNeonCount = initialNeonCount;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasWon)
        {
            Debug.Log(winTransition);
            winTransition -= Time.deltaTime;
            backgroundImage.color = new Color(1, 1, 1, 1 - winTransition / WINTRANSITIONTIME);
            if (winTransition < 0)
            {
                switch (SceneManager.GetActiveScene().name)
                {
                    case "01_Level_TUTO":
                        PlayerPrefs.SetFloat("Photo0", 1);
                        SceneManager.LoadScene("MainMenu");
                        break;
                    case "02_Level_Cliff":
                        PlayerPrefs.SetFloat("Photo1", 1);
                        SceneManager.LoadScene("MainMenu");
                        break;
                    case "03_Level_Hole":
                        PlayerPrefs.SetFloat("Photo2", 1);
                        SceneManager.LoadScene("MainMenu");
                        break;
                    default:
                        SceneManager.LoadScene("MainMenu");
                        break;
                }
            }
        }
        // TODO : Remove
        if (Input.GetKeyDown("n"))
        {
            Debug.Log("Cheat code activated");
            DayNightManager.SetDay(!DayNightManager.GetDay());
        }
        
        GetComponentInChildren<InteractionCollider>().canInteract(DayNightManager.GetDay());
        
        if (Input.GetButtonDown("Fire2") && !GetComponent<CharacterMovement>().IsJumping()
            && !storm.IsFalling())
        {
            Queue<GameObject> items = new Queue<GameObject>(interactibles);
            if (items.Count > 0)
            {
                GameObject item = items.Dequeue();
                if (item.CompareTag("Neon") && DayNightManager.GetDay())
                {
                    RemoveNeon(item);
                }
                if (item.CompareTag("Door") && DayNightManager.GetDay())
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
            && !storm.IsFalling() && DayNightManager.GetDay())
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Home"))
        {
            hasWon = true;
            winTransition = WINTRANSITIONTIME;
        }
    }
}
