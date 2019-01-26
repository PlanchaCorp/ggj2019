using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DayNightManager
{
    private static bool day;
    public static GameObject[] neons;
    private static GameObject sun;



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
  public static void InitDay()
    {
        day = true;
        sun = GameObject.Find("Sun");
        neons = GameObject.FindGameObjectsWithTag("Neon");
        ChangeCycle();
    }

    private static void ChangeCycle()
    {
        sun.SetActive(day);
        if (day)
        {
            foreach(GameObject neon in neons)
            {
                LigthController neonLigth = neon.GetComponent<LigthController>();
                neonLigth.SetDayLigthColor();
            }
           
        } else
        {
            foreach (GameObject neon in neons)
            {
                LigthController neonLigth = neon.GetComponent<LigthController>();
                neonLigth.SetNightLightColor();
            }
        }
    }




    public static void SetDay(bool day) {
        DayNightManager.day = day;
        ChangeCycle();
    }

    public static bool GetDay()
    {
        return day;
    }
}
