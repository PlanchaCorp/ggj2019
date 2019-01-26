using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    float picture0, picture1, picture2, picture3;

    // Start is called before the first frame update
    void Start()
    {
        picture0 = PlayerPrefs.GetFloat("Photo0");
        picture1 = PlayerPrefs.GetFloat("Photo1");
        picture2 = PlayerPrefs.GetFloat("Photo2");
        picture3 = PlayerPrefs.GetFloat("Photo3");
        ColorizePictures();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ColorizePictures()
    {
        if (picture0 > 0)
        {
            transform.Find("Photo0_Color").gameObject.SetActive(true);
        }
        if (picture1 > 0)
        {
            transform.Find("Photo1_Color").gameObject.SetActive(true);
        }
        if (picture2 > 0)
        {
            transform.Find("Photo2_Color").gameObject.SetActive(true);
        }
        if (picture3 > 0)
        {
            transform.Find("Photo3_Color").gameObject.SetActive(true);
        }
    }

    public void StartLevel0()
    {
        SceneManager.LoadScene("Scenes/LD/01_Level_TUTO");
    }

    public void StartLevel1()
    {
        SceneManager.LoadScene("Scenes/LD/02_Level_Cliff");
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene("Scenes/LD/033_Level_Hole");
    }

    public void StartLevel3()
    {
        Debug.Log("Level 3");
    }
}
