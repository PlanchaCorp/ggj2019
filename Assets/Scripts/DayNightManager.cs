using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DayNightManager
{
    private static bool day;
    public static GameObject[] neons;
    private static GameObject sun;
    private static GameObject[] snowLayers;



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitDay()
    {
        day = true;
        sun = GameObject.Find("Sun");
        snowLayers = GameObject.FindGameObjectsWithTag("Snow");
        neons = GameObject.FindGameObjectsWithTag("Neon");
        if(sun != null && snowLayers != null)
            ChangeCycle();
    }

    public static void ChangeCycle()
    {
        sun.SetActive(day);
        if (day)
        {
            GameObject.Find("PlayerAndCamera/UI/Day").SetActive(true);
            GameObject.Find("PlayerAndCamera/UI/Night").SetActive(false);
            GameObject.Find("PlayerAndCamera/UI/SnowStorm").GetComponentInChildren<ParticleSystem>().Stop();
            foreach(GameObject snowLayer in snowLayers)
            {
                snowLayer.SetActive(false);
            }
            foreach (GameObject neon in neons)
            {
                LigthController neonLigth = neon.GetComponent<LigthController>();
                neonLigth.SetDayLigthColor();
            }
           
        } else
        {
            GameObject.Find("PlayerAndCamera/UI/Day").SetActive(false);
            GameObject.Find("PlayerAndCamera/UI/Night").SetActive(true);
            GameObject.Find("PlayerAndCamera/UI/SnowStorm").GetComponentInChildren<ParticleSystem>().Simulate(1);
            GameObject.Find("PlayerAndCamera/UI/SnowStorm").GetComponentInChildren<ParticleSystem>().Play();
            foreach (GameObject snowLayer in snowLayers)
            {
                snowLayer.SetActive(true);
            }
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
