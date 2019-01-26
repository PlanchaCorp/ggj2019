using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigthController : MonoBehaviour
{

    private Color neonColor;
    // Start is called before the first frame update

    void Start()
    {
        neonColor = Color.magenta;
        neonColor = NeonColor.Colors[Mathf.FloorToInt(Random.Range(0, 3))];
        Debug.Log(neonColor.ToString());
        this.GetComponent<SpriteRenderer>().color = neonColor;   
        SetDayLigthColor();
    }

    public void SetNightLightColor()
    {
        
        this.GetComponentInChildren<Light>().color = new Color(255,172,193);
        this.GetComponentInChildren<Light>().intensity = 9;
    }
    public void SetDayLigthColor()
    {
        this.GetComponentInChildren<Light>().color = Color.white;
        this.GetComponentInChildren<Light>().intensity = 5;
    }


}
