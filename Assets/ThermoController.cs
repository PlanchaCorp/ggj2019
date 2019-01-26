using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThermoController : MonoBehaviour
{
    Storm storm;
    // Start is called before the first frame update
    void Start()
    {
        storm = GameObject.FindGameObjectWithTag("Storm").GetComponent<Storm>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = storm.GetRemainingTimeRatio();
        GameObject.Find("PlayerAndCamera/UI/Night/Thermometer").GetComponent<Image>().fillAmount = time;
    }
}
